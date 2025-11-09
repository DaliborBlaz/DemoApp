using DemoApp.Application.Common.Interfaces;
using DemoApp.Application.Models.Auth;
using Microsoft.AspNetCore.Mvc;

namespace DemoApp.API.Controllers;


[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    
    public AuthController(IAuthService authService)
    {
        _authService = authService; 
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var result = await _authService.RegisterAsync(request.Email, request.Password, request.FullName);

        if (result.Success)
            return Ok(result);
        
        if (string.Equals(result.Message, "User already exists", StringComparison.OrdinalIgnoreCase))
            return Conflict(result);
        

        return BadRequest(result);
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var result = await _authService.LoginAsync(request.Email, request.Password);

        if (!result.Success)
            return Unauthorized(result);
        

        return Ok(result);
    }
}