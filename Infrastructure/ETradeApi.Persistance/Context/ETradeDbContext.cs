using ETradeApi.Domain;
using ETradeApi.Domain.Common;
using ETradeApi.Domain.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeApi.Persistence.Context
{
    public class ETradeDbContext : IdentityDbContext<AppUser,AppRole,string>
    {
        public ETradeDbContext(DbContextOptions options):base(options)
        {

        }
        
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var datas = ChangeTracker.Entries<BaseEntity>();

            foreach (var entry in datas)
            {
                if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedDate = DateTime.Now;
                }
                else if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedDate = DateTime.Now;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

    }
}
