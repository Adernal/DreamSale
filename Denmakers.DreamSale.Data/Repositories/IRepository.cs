using Denmakers.DreamSale.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Denmakers.DreamSale.Data.Repositories
{
    public partial interface IRepository<T> where T : BaseEntity
    {
        IQueryable<T> Table { get; }

        IQueryable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties);
        IQueryable<T> All { get; }
        IQueryable<T> GetAll();
        T GetById(object id);
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
        void Insert(T entity);
        void Delete(T entity);
        void Update(T entity);

        void Insert(IEnumerable<T> entities);
        void Update(IEnumerable<T> entities);
        void Delete(IEnumerable<T> entities);

        /// <summary>
        /// Gets a table with "no tracking" enabled (EF feature) Use it only when you load record(s) only for read-only operations
        /// </summary>
        IQueryable<T> TableNoTracking { get; }
    }
}

