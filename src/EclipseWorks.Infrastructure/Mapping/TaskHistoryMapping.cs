using EclipseWorks.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EclipseWorks.Infrastructure.Mapping
{
    public class TaskHistoryMapping : IEntityTypeConfiguration<TaskHistory>
    {
        public void Configure(EntityTypeBuilder<TaskHistory> builder)
        {
            builder.ToTable("TASK_HISTORY");


            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .HasColumnName("ID")
                .ValueGeneratedOnAdd();

            builder.Property(x => x.TaskId)
                .HasColumnName("TASK_ID")
                .IsRequired();

            builder.Property(x => x.UpdatedByUser)
                .IsRequired()
                .ValueGeneratedNever()
                .HasColumnName("UPDATED_BY_USER")
                .HasColumnType("VARCHAR(300)");

            builder.Property(x => x.CreatedAt)
                .HasColumnName("CREATED_AT")
                .HasColumnType("timestamp with time zone")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(x => x.UpdatedAt)
              .HasColumnName("UPDATED_AT")
              .HasColumnType("timestamp with time zone")
              .IsRequired(false);


            builder.HasMany(x => x.Changes)
                .WithOne(x => x.History)
                .HasForeignKey(x => x.HistoryId);

            builder.HasOne(x => x.Task)
                .WithMany(x => x.Histories)
                .HasForeignKey(x => x.TaskId);


        }
    }
}
