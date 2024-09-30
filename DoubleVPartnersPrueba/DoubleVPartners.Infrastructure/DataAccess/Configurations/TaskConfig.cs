using DoubleVPartners.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DoubleVPartners.Infrastructure.DataAccess.Configurations
{
    public class TaskConfig : IEntityTypeConfiguration<Domain.Entities.Task>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Task> builder)
        {
            builder.ToTable("tblTask");
            builder.HasKey(x => x.IdTask);

            builder.Property(x => x.IdTask)
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(x => x.NameTask)
                .HasColumnName("NameTask")
                .HasMaxLength(50)
                .IsRequired(true);

            builder.Property(x => x.Description)
                .HasColumnName("Description")
                .HasMaxLength(100)
                .IsRequired(true);

            builder.Property(x => x.StatusTask)
                .HasColumnName("StatusTask") 
                .HasMaxLength(20)
                .IsRequired(true);

            builder.Property(x => x.CreateDate)
                .HasColumnName("CreateDate")
                .IsRequired(true);


            builder.HasOne(x => x.User)
                .WithMany(x => x.Tasks)
                .HasForeignKey(x => x.IdUser);

        }
    }
}
