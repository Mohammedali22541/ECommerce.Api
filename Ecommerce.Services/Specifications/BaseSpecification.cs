using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Services.Specifications
{
    public abstract class BaseSpecification<TEntity, TKey> : ISpecification<TEntity, TKey>
         where TEntity : BaseEntity<TKey>
    {

        protected BaseSpecification(Expression<Func<TEntity , bool>> criteria)
        {
            Criteria = criteria;
        }
        public ICollection<Expression<Func<TEntity, object>>> Includes { get; } = [];

        public Expression<Func<TEntity, bool>> Criteria { get; }

        protected void AddInclude(Expression<Func<TEntity,object>> IncludeExpression)
        {
            Includes.Add(IncludeExpression);
        }
    }
}
