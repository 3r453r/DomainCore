using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DomainObjects.Repository
{
    public interface IRepository<TEntity>
    {
        void Add(TEntity entity);
        void Remove(TEntity entity);
        TEntity Get(string id);
    }
}
