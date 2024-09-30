using DoubleVPartners.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DoubleVPartners.Infrastructure.DataAccess.Configurations
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("tblUser");
            builder.HasKey(x => x.IdUser);

            builder.Property(x => x.IdUser)
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(x => x.Name)
                .HasColumnName("Name")
                .HasMaxLength(100)
                .IsRequired(true);

            builder.Property(x => x.Email)
                .HasColumnName("Email")
                .HasMaxLength(100)
                .IsRequired(true);

            builder.Property(x => x.PhoneNumber)
                .HasColumnName("PhoneNumber")
                .HasMaxLength(15)
                .IsRequired(true);

            builder.Property(x => x.Password)
                .HasColumnName("Password")
                .HasMaxLength(500)
                .IsRequired(true);

            builder.Property(e => e.IsActive)
                .HasColumnName("IsActive");

            builder.HasOne(x => x.Role)
                .WithMany(x => x.Users)
                .HasForeignKey(x => x.IdRole);
        }
    }
}
