using ETradeApi.Application.Repositories;
using ETradeApi.Domain.Common;
using ETradeApi.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ETradeApi.Persistence.Repositories
{
    public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
    {
        private readonly ETradeDbContext _context;
        public ReadRepository(ETradeDbContext context)
        {
            _context = context;
        }
        public DbSet<T> Table => _context.Set<T>();

        public IQueryable<T> GetAll(bool tracking = true)
        {
            var query = Table.AsQueryable();
            if (!tracking)
                query.AsNoTracking();
            return query;
        }

        public async Task<T> GetByIdAsync(string Id, bool tracking = true)
        {
            var query = Table.AsQueryable();
            if (!tracking)
                query.AsNoTracking();
            return await query.FirstOrDefaultAsync(i => i.ID == Guid.Parse(Id));
        }

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> method, bool tracking = true)
        {
            var query = Table.AsQueryable();
            if(!tracking)
                query = query.AsNoTracking();
            return await query.FirstOrDefaultAsync(method);
        }

        public IQueryable<T> GetWhere(Expression<Func<T, bool>> method, bool tracking = true)
        {
            var query = Table.Where(method);
            if (!tracking)
                query = query.AsNoTracking();
            return query;
        }
    }
}
