using System;

namespace Infrastructure.Data.Abstractions
{
    public interface IRepository { }

    public interface IRepository<TEntity, TKey> :
        IRepository,
        IQuery<TEntity, TKey>,
        ICommand<TEntity, TKey>
    { }

    [Obsolete("Use IRepository<TEntity, TKey> instead.")]
    public interface IRepository<TEntity> :
        IRepository,
        IQuery<TEntity>,
        ICommand<TEntity>
    { }
}
