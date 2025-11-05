using DemoApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DemoApp.Infrastructure.Persistence.Configurations;


public class RoleConfigurations : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(x=>x.Id);
        builder.Property(x=>x.Name).IsRequired().HasMaxLength(50);
        builder.HasData(
            new Role { Id = Guid.Parse("00000000-0000-0000-0000-000000000001"), Name = "Admin" },
            new Role { Id = Guid.Parse("00000000-0000-0000-0000-000000000002"), Name = "Manager" },
            new Role { Id = Guid.Parse("00000000-0000-0000-0000-000000000003"), Name = "Member" }
        );
    }
}