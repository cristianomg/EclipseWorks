using EclipseWorks.Domain.Entities;
using EclipseWorks.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EclipseWorks.Infrastructure.Mapping
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("USER");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .HasColumnName("ID")
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Name)
                .HasColumnType("VARCHAR(100)")
                .HasColumnName("NAME")
                .IsRequired();

            builder.Property(x => x.Role)
                .IsRequired()
                .HasColumnName("ROLE")
                .HasConversion(
                  v => v.ToString(),
                  v => (Role)Enum.Parse(typeof(Role), v)
                );

            builder.Property(x => x.CreatedAt)
              .HasColumnName("CREATED_AT")
              .HasColumnType("timestamp with time zone")
              .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(x => x.UpdatedAt)
              .HasColumnName("UPDATED_AT")
              .HasColumnType("timestamp with time zone")
              .IsRequired(false);


            builder.HasData(
                new User(1, "Usuario 1", Domain.Enums.Role.User),
                new User(2, "Usuario 2", Domain.Enums.Role.User),
                new User(3, "Usuario 3", Domain.Enums.Role.User),
                new User(4, "Usuario 4", Domain.Enums.Role.User),
                new User(5, "Usuario Gerente 1", Domain.Enums.Role.Manager)
            );
        }
    }
}
