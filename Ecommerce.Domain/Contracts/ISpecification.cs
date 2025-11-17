using ECommerce.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Contracts
{
    public interface ISpecification<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {
        ICollection<Expression<Func<TEntity, object>>> Includes { get; }
        Expression<Func<TEntity, bool>> Criteria { get; }
        Expression<Func<TEntity, object>> AddOrderBy { get; }
        Expression<Func<TEntity, object>> AddOrderByDes { get; }

         int Take { get;}
         int Skip { get;}
         bool IsPaginated { get;}



    }
}
