using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entity;
using ECommerce.Persistence.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Persistence.Data.Repositories
{
    public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        private readonly StoreDbContext _dbContext;

        public GenericRepository(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddAsync(TEntity entity)
        {
           await _dbContext.Set<TEntity>().AddAsync(entity);
        }

        public void Delete(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()=> await _dbContext.Set<TEntity>().ToListAsync();

        public async Task<IEnumerable<TEntity>> GetAllAsync(ISpecification<TEntity, TKey> specification)
        {
            var Query = SpecificationEvaluator.CreateQuery(_dbContext.Set<TEntity>() , specification);

            return await Query.ToListAsync();
        }

        public async Task<TEntity?> GetByIdAsync(TKey id) => await _dbContext.Set<TEntity>().FindAsync(id);

        public Task<TEntity?> GetByIdAsync(ISpecification<TEntity, TKey> specification)
        {
            var Query = SpecificationEvaluator.CreateQuery(_dbContext.Set<TEntity>(), specification);
            return Query.FirstOrDefaultAsync();
        }

        public void Update(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
        }
    }
}
