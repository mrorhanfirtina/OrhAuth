using OrhAuth.Models.Entities.Base;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;

namespace OrhAuth.Data.Repositories.Abstract
{
    public interface IEntityRepository<T> where T : EntityBase
    {
        T Get(Expression<Func<T, bool>> filter);
        IList<T> GetList(Expression<Func<T, bool>> filter = null);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
