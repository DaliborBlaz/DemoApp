using DemoApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DemoApp.Infrastructure.Persistence.Configurations;

public class UserConfigurations:IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Email).IsRequired().HasMaxLength(100);
        builder.Property(x=>x.PasswordHash).IsRequired();
        builder.Property(x=>x.FullName).IsRequired().HasMaxLength(100);
        
        builder.HasOne(x=>x.Role).WithMany(r=>r.Users).HasForeignKey(x=>x.RoleId);
    }
}