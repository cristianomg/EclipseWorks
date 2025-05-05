using TaskManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TaskManager.Infrastructure.Mapping
{
    public class ProjectMapping : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.ToTable("PROJECT");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .HasColumnName("ID")
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Name)
                .HasColumnType("VARCHAR(100)")
                .HasColumnName("NAME")
                .IsRequired();

            builder.HasIndex(x => new { x.Name, x.UserId })
                .IsUnique();

            builder.Property(x => x.UserId)
                .HasColumnName("USER_ID")
                .IsRequired();

            builder.Property(x => x.CreatedAt)
              .HasColumnName("CREATED_AT")
              .HasColumnType("timestamp with time zone")
              .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(x => x.UpdatedAt)
              .HasColumnName("UPDATED_AT")
              .HasColumnType("timestamp with time zone")
              .IsRequired(false);

            builder.HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .IsRequired();

            builder.HasMany(x => x.Tasks)
                .WithOne(x => x.Project)
                .HasForeignKey(x => x.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
