
using DoubleVPartners.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DoubleVPartners.Infrastructure.DataAccess.Configurations
{
    public class RoleConfig : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("tblRole");
            builder.HasKey(x => x.IdRole);

            builder.Property(x => x.IdRole)
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(x => x.NameRole)
                .HasColumnName("NameRole")
                .HasMaxLength(15)
                .IsRequired(true);

            builder.HasMany(x => x.Users)
                .WithOne(x => x.Role);
        }
    }
}
