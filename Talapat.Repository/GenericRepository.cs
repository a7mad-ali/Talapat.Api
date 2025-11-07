using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Specifications;
using Talapat.Infrastructure.Helpers.SpacificationsEvaluator;
using Talapat.Repository.Data;

namespace Talapat.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        public readonly TalabatDbContext _dbContext;
        public readonly DbSet<T> _db;
        public GenericRepository(TalabatDbContext dbContext)
        {
            _dbContext = dbContext;
            _db = _dbContext.Set<T>();

        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _db.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllWithSpecAsync(ISpecification<T> spec)
        {
            return await applySpecification(spec).AsNoTracking().ToListAsync();
        }

        public async Task<T?> GetAsync(int id)
        {
            return await _db.FindAsync(id);
        }

        public async Task<T?> GetWithSpecAsync(ISpecification<T> spec)
        {
            return await applySpecification(spec).FirstOrDefaultAsync();
        }
        private IQueryable<T> applySpecification(ISpecification<T> spec)
        {
            return SpecificationsEvaluator<T>.GetQuery(_db, spec);


        }
    }
}
