using DemoApp.Application.Models;

namespace DemoApp.Aplication.Common.Interfaces;

public interface IAuthService
{
    Task<AuthResult> RegisterAsync(string email, string password, string fullName);
    Task<AuthResult> LoginAsync(string email, string password);
}