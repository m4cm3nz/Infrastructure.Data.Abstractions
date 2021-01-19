using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infrastructure.Data.Abstractions
{
    public interface IQuery { }

    public interface IGetAll<TEntity> : IQuery
    {
        Task<IEnumerable<TEntity>> GetAll();
    }

    public interface IGetAllByExpression<TEntity> : IQuery
    {
        Task<IEnumerable<TEntity>> GetAll(Expression<Func<TEntity, bool>> predicate);
    }

    public interface IGetById<TEntity> : IQuery
    {
        Task<TEntity> GetByID(dynamic identity);
    }

    public interface IFindByID : IQuery
    {
        Task<bool> FindByID(dynamic identity);
    }

    public interface IQuery<TEntity> : 
        IGetAll<TEntity>, 
        IGetAllByExpression<TEntity>, 
        IGetById<TEntity>, 
        IFindByID
    {
    };
}
