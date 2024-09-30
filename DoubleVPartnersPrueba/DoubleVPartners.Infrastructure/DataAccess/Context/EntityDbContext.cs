using DoubleVPartners.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace DoubleVPartners.Infrastructure.DataAccess.Context
{
    public class EntityDbContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public EntityDbContext(
            DbContextOptions<EntityDbContext> dbContextOptions, IConfiguration configuration) : base(dbContextOptions)
        {
            _configuration = configuration;
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("dbo");
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
            Seed(modelBuilder);
        }

        private void Seed(ModelBuilder modelBuilder)
        {
            var listRole = new List<Domain.Entities.Role>
            {
                new Domain.Entities.Role { IdRole = 1, NameRole = "Administrador"  },
                new Domain.Entities.Role { IdRole = 2, NameRole = "Supervisor"  },
                new Domain.Entities.Role { IdRole = 3, NameRole = "Empleado"  }
            };
            var userAdmin = new Domain.Entities.User
            {
                IdUser = 1,
                IdRole = 1,
                Name = "Admin",
                Email = "admin@admin.com",
                Password = "PjJ2Yv6RFgk=",
                PhoneNumber = "1234567890",
                IsActive = true
            };

            modelBuilder.Entity<Role>().HasData(listRole);
            modelBuilder.Entity<Domain.Entities.User>().HasData(userAdmin);
        }
    }
}
