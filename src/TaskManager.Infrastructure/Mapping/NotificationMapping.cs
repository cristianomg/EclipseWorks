using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Domain.Entities;

namespace TaskManager.Infrastructure.Mapping
{
    public class NotificationMapping : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.ToTable("NOTIFICATION");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .HasColumnName("ID");

            builder.Property(x => x.Value)
                .HasColumnType("VARCHAR(1000)")
                .HasColumnName("VALUE")
                .IsRequired();


            builder.Property(x => x.UserId)
                .IsRequired()
                .HasColumnName("USER_ID");

            builder.Property(x => x.Read)
                .IsRequired()
                .HasDefaultValue(false)
                .HasColumnName("READ");

            builder.Property(x => x.ReadAt)
                .IsRequired(false)
                .HasColumnName("READ_AT")
                .HasColumnType("timestamp with time zone");

            builder.HasOne(x=>x.User)
                .WithMany(x=>x.Notifications)
                .HasForeignKey(x=>x.UserId);    
        }
    }
}
