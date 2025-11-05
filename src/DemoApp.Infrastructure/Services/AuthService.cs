using DemoApp.Application.Models;
using DemoApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using DemoApp.Aplication.Common.Interfaces;
using DemoApp.Domain.Entities;

namespace DemoApp.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly DemoAppDbContext _context;
        private readonly JwtSettings _jwtSettings;

        public AuthService(DemoAppDbContext context, IOptions<JwtSettings> jwtOptions)
        {
            _context = context;
            _jwtSettings = jwtOptions.Value;
        }

        public async Task<AuthResult> RegisterAsync(string email, string password, string fullName)
        {
            // Check if user exists
            if (await _context.Users.AnyAsync(u => u.Email == email))
            {
                return new AuthResult { Success = false, Message = "User already exists" };
            }

            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = email,
                FullName = fullName,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
                RoleId = Guid.Parse("00000000-0000-0000-0000-000000000003") // Member
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return GenerateAuthResult(user);
        }

        public async Task<AuthResult> LoginAsync(string email, string password)
        {
            var user = await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Email == email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                return new AuthResult { Success = false, Message = "Invalid credentials" };
            }

            return GenerateAuthResult(user);
        }

        private AuthResult GenerateAuthResult(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtSettings.Key);
            var expires = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role?.Name ?? "Member"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = expires,
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new AuthResult
            {
                Success = true,
                Token = tokenHandler.WriteToken(token),
                ExpiresAt = expires
            };
        }
    }
}
