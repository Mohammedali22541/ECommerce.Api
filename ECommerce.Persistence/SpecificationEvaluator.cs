using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Persistence
{
    public static class SpecificationEvaluator
    {
        public static IQueryable<TEntity> CreateQuery<TEntity , TKey>(IQueryable<TEntity>
            EntryPoint , ISpecification<TEntity , TKey> specification) where TEntity : BaseEntity<TKey>
        {

            var Query = EntryPoint;

            if (specification is not null)
            {
                if (specification.Criteria is not null)
                {
                    Query = Query.Where(specification.Criteria);
                }
                if (specification.Includes is not null && specification.Includes.Any())
                {
                    Query = specification.Includes.Aggregate(Query, (CurrentQuery, includeExp) =>
                    CurrentQuery.Include(includeExp));
                }
                
            }
            
            return Query;
        }
    }
}
