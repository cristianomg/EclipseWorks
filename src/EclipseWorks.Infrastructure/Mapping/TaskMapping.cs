using EclipseWorks.Domain.Entities;
using EclipseWorks.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EclipseWorks.Infrastructure.Mapping
{
    public class TaskMapping : IEntityTypeConfiguration<Tasks>
    {
        public void Configure(EntityTypeBuilder<Tasks> builder)
        {
            builder.ToTable("TASK");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("ID")
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Title)
                .HasColumnName("TITLE")
                .HasColumnType("VARCHAR(100)")
                .IsRequired();

            builder.Property(x => x.Description)
                .HasColumnName("DESCRIPTION")
                .HasColumnType("VARCHAR(500)")
                .IsRequired(false);

            builder.Property(x => x.Status)
                .IsRequired()
                .HasColumnName("STATUS")
                .HasDefaultValue(TasksStatus.Pending)
                .HasConversion(
                  v=>v.ToString(),
                  v=> (TasksStatus)Enum.Parse(typeof(TasksStatus), v)
                );

            builder.Property(x => x.Priority)
                .IsRequired()
                .HasColumnName("PRIORITY")
                .HasConversion(
                  v => v.ToString(),
                  v => (TaskPriority)Enum.Parse(typeof(TaskPriority), v)
                );

            builder.Property(x => x.DueDate)
                .IsRequired()
                .HasColumnName("DUE_DATE")
                .HasColumnType("timestamp with time zone");

            builder.Property(x => x.ProjectId)
                .HasColumnName("PROJECT_ID")
                .IsRequired();

            builder.Property(x => x.CreatedAt)
              .HasColumnName("CREATED_AT")
              .HasColumnType("timestamp with time zone")
              .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(x => x.UpdatedAt)
              .HasColumnName("UPDATED_AT")
              .HasColumnType("timestamp with time zone")
              .IsRequired(false);

            builder.HasOne(x => x.Project)
                .WithMany(x => x.Tasks)
                .HasForeignKey(x => x.ProjectId);

            builder.HasMany(x => x.Histories)
                .WithOne(x => x.Task)
                .HasForeignKey(x => x.TaskId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.Comments)
                .WithOne()
                .HasForeignKey(x => x.TaskId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
