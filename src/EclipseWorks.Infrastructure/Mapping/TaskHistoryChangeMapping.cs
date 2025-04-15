using EclipseWorks.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EclipseWorks.Infrastructure.Mapping
{
    public class TaskHistoryChangeMapping : IEntityTypeConfiguration<TaskHistoryChange>
    {
        public void Configure(EntityTypeBuilder<TaskHistoryChange> builder)
        {
            builder.ToTable("TASK_HISTORY_CHANGE");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .HasColumnName("ID")
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Field)
                .HasColumnName("FIELD")
                .HasColumnType("VARCHAR(100)")
                .IsRequired(true);

            builder.Property(x => x.OldValue)
                .HasColumnName("OLD_VALUE")
                .IsRequired(false);

            builder.Property(x => x.NewValue)
                .HasColumnName("NEW_VALUE")
                .IsRequired(true);

            builder.Property(x => x.HistoryId)
                .HasColumnName("TASK_HISTORY_ID");

            builder.Property(x => x.CreatedAt)
                .HasColumnName("CREATED_AT")
                .HasColumnType("timestamp with time zone")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(x => x.UpdatedAt)
              .HasColumnName("UPDATED_AT")
              .HasColumnType("timestamp with time zone")
              .IsRequired(false);

            builder.HasOne(x => x.History)
                .WithMany(x => x.Changes)
                .HasForeignKey(x => x.HistoryId);
        }
    }
}
