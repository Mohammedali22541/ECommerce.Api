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
                if (specification.AddOrderBy is not null)
                {
                   Query = Query.OrderBy(specification.AddOrderBy);
                }
                if (specification.AddOrderByDes is not null)
                {
                    Query = Query.OrderByDescending(specification.AddOrderByDes);   
                }
                if (specification.IsPaginated == true)
                {
                    Query = Query.Skip(specification.Skip).Take(specification.Take);
                }

            }
            
            return Query;
        }
    }
}
