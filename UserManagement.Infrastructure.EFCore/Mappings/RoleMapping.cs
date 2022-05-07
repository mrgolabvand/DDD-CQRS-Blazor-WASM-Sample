using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserManagement.Domain.Aggregates.Users.ValueObjects;

namespace UserManagement.Infrastructure.EFCore.Mappings
{
    public class RoleMapping : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Roles");

            builder.HasKey(v => v.Value);

            builder.Property(v => v.Value)
                .ValueGeneratedNever()
                .IsRequired();

            builder.Property(v => v.Name)
                .HasMaxLength(Role.MaxLength)
                .IsRequired();

            builder.HasData(Role.User);
            builder.HasData(Role.Blogger);
            builder.HasData(Role.Admin);
        }
    }
}
