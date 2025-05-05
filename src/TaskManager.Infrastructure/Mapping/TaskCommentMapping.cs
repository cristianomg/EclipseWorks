using TaskManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Infrastructure.Mapping
{
    public class TaskCommentMapping : IEntityTypeConfiguration<TaskComment>
    {
        public void Configure(EntityTypeBuilder<TaskComment> builder)
        {
            builder.ToTable("TASK_COMMENT");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .HasColumnName("ID")
                .ValueGeneratedOnAdd();

            builder.Property(x => x.CreatedAt)
                .HasColumnName("CREATED_AT")
                .HasColumnType("timestamp with time zone")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(x => x.UpdatedAt)
              .HasColumnName("UPDATED_AT")
              .HasColumnType("timestamp with time zone")
              .IsRequired(false);

            builder.Property(x => x.Value)
                .IsRequired()
                .HasColumnName("VALUE")
                .HasColumnType("VARCHAR(500)");

            builder.Property(x => x.TaskId)
                .IsRequired()
                .HasColumnName("TASK_ID");
        }
    }
}
