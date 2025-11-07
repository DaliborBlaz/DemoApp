using DemoApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DemoApp.Infrastructure.Persistence.Configurations
{
    public class WorkItemConfiguration : IEntityTypeConfiguration<WorkItem>
    {
        public void Configure(EntityTypeBuilder<WorkItem> builder)
        {
            builder.HasKey(w=> w.Id);
            
            builder.Property(w => w.Title)
                .IsRequired()
                .HasMaxLength(100);
            
            builder.Property(w => w.Description)
                .HasMaxLength(1000);
            
            builder.Property(w => w.AssignedTo)
                .HasMaxLength(100);
        }
    }
}
