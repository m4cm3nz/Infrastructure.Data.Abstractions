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

    public interface IGetById<TEntity, TKey> : IQuery
    {
        Task<TEntity> GetByID(TKey identity);
    }

    public interface IFindByID<TKey> : IQuery
    {
        Task<bool> FindByID(TKey identity);
    }

    public interface IQuery<TEntity, TKey> :
        IGetAll<TEntity>,
        IGetAllByExpression<TEntity>,
        IGetById<TEntity, TKey>,
        IFindByID<TKey>
    { }

    [Obsolete("Use IGetById<TEntity, TKey> instead.")]
    public interface IGetById<TEntity> : IQuery
    {
        Task<TEntity> GetByID(dynamic identity);
    }

    [Obsolete("Use IFindByID<TKey> instead.")]
    public interface IFindByID : IQuery
    {
        Task<bool> FindByID(dynamic identity);
    }

    [Obsolete("Use IQuery<TEntity, TKey> instead.")]
    public interface IQuery<TEntity> :
        IGetAll<TEntity>,
        IGetAllByExpression<TEntity>,
        IGetById<TEntity>,
        IFindByID
    { }
}
