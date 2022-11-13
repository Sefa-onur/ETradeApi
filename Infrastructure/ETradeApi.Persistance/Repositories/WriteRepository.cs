using ETradeApi.Application.Repositories;
using ETradeApi.Domain.Common;
using ETradeApi.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeApi.Persistence.Repositories
{
    public class WriteRepository<T> : IWriteRepository<T> where T : BaseEntity
    {
        private readonly ETradeDbContext _context;
        public WriteRepository(ETradeDbContext context)
        {
            _context = context;
        }
        public DbSet<T> Table => _context.Set<T>();

        public async Task<bool> AddAsync(T model)
        {
            EntityEntry<T> entity = await Table.AddAsync(model);
            return entity.State == EntityState.Added;
        }

        public async Task<bool> AddRangeAsync(List<T> models)
        {
            await Table.AddRangeAsync(models);
            return true;
        }

        public bool Remove(T model)
        {
            EntityEntry<T> entry = Table.Remove(model);
            return entry.State == EntityState.Deleted;
        }

        public async Task<bool> RemoveAsync(string Id)
        {
            T model = await Table.FirstOrDefaultAsync(i => i.ID == Guid.Parse(Id));
            return Remove(model);
        }

        public bool RemoveRange(List<T> models)
        {
            Table.RemoveRange(models);
            return true;
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public bool Update(T model)
        {
            EntityEntry entry = Table.Update(model);
            return entry.State == EntityState.Modified;
        }
    }
}
