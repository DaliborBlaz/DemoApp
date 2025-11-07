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
        try
        {
            var result = await _authService.RegisterAsync(request.Email, request.Password, request.FullName);
            return Ok(result);
        }
        catch(Exception ex)
        {
                return UnprocessableEntity(new { message = ex.Message });
        }
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        try
        {
            var result = await _authService.LoginAsync(request.Email, request.Password);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
    }
}