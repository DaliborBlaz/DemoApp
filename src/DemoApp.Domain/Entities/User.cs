namespace DemoApp.Domain.Entities;

    public class User
    {
        public Guid Id { get; set; }
        public required string Email { get; set; }
        public required string PasswordHash { get; set; }
        public required string FullName { get; set; }
        public Guid RoleId { get; set; }
        
        public Role? Role { get; set; }
    }
