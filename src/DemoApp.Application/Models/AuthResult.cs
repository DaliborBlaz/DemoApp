namespace DemoApp.Application.Models
{
    public class AuthResult
    {
        public string Token { get; set; } = string.Empty;

        public DateTime? ExpiresAt { get; set; } = null;
        
        public bool Success { get; set; } = true;
        public string? Message { get; set; }
    }
}