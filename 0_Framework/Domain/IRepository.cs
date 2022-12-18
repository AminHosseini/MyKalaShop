using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace _0_Framework.Domain
{
    public interface IRepository<TKey, T> where T : class
    {
        void Create(T entity);
        T Get(TKey id);
        List<T> GetAll();
        bool Exists(Expression<Func<T, bool>> expression);
        void Save();
    }
}
