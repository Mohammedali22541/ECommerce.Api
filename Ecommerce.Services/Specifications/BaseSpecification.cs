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

        protected BaseSpecification(Expression<Func<TEntity, bool>> criteria)
        {
            Criteria = criteria;
        }
        public ICollection<Expression<Func<TEntity, object>>> Includes { get; } = [];

        public Expression<Func<TEntity, bool>> Criteria { get; }

        public Expression<Func<TEntity, object>> AddOrderBy { private set;  get; }

        public Expression<Func<TEntity, object>> AddOrderByDes { private set; get;}
        public int Take { get ; private set; }
        public int Skip { get; private set; }
        public bool IsPaginated { get; private set; }

        protected void ApplyPagination(int pageIndex , int pageSize)
        {
            IsPaginated = true;
            Take = pageSize;
            Skip = (pageIndex -1) * pageSize;
            

        }
        protected void AddInclude(Expression<Func<TEntity,object>> IncludeExpression)
        {
            Includes.Add(IncludeExpression);
        }

        protected void AddOrderByAsc(Expression<Func<TEntity , object>> orderbyasc)
        {
            AddOrderBy = orderbyasc;
        }
        protected void AddOrderByDesc(Expression<Func<TEntity, object>> orderbyDesc)
        {
            AddOrderByDes = orderbyDesc;
        }
    }
}
