namespace DemoApp.Domain.Entities;


    public class Role
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default;  
        
        public ICollection<User> Users { get; set; } = new List<User>();
    }
