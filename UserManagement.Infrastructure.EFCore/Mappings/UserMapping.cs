using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserManagement.Domain.Aggregates.Users;
using UserManagement.Domain.Aggregates.Users.ValueObjects;
using UserManagement.Domain.SharedKernel;

namespace UserManagement.Infrastructure.EFCore.Mappings
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(x => x.Id);

            builder.Property(v => v.FullName)
                .HasConversion(v => v.Value, v => FullName.Create(v).Value)
                .HasMaxLength(FullName.MaxLength)
                .IsRequired();

            builder.Property(v => v.UserName)
                .HasConversion(v => v.Value, v => UserName.Create(v).Value)
                .HasMaxLength(UserName.MaxLength)
                .IsRequired();

            builder.Property(v => v.Email)
                .HasConversion(v => v.Value, v => Email.Create(v).Value)
                .HasMaxLength(Email.MaxLength)
                .IsRequired();

            builder.Property(v => v.Password)
                .HasConversion(v => v.Value, v => Password.Create(v).Value)
                .HasMaxLength(Password.DbMaxLength)
                .IsRequired();

            builder.Property(v => v.PhoneNumber)
                .HasConversion(v => v.Value, v => PhoneNumber.Create(v).Value)
                .HasMaxLength(PhoneNumber.FixLength)
                .IsRequired();

            builder.HasOne(v => v.Role)
                .WithMany()
                .HasForeignKey("RoleId")
                .IsRequired();

            builder.Property<int>("RoleId")
                .HasColumnName("RoleId")
                .IsRequired()
                .UsePropertyAccessMode(PropertyAccessMode.Field);

        }
    }
}
