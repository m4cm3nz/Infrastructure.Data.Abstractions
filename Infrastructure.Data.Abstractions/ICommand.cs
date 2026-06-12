using System;
using System.Threading.Tasks;

namespace Infrastructure.Data.Abstractions
{
    public interface ICommand { }

    public interface IAdd<TEntity, TKey> : ICommand
    {
        Task<TKey> Add(TEntity entity);
    }

    public interface IUpdate<TEntity, TKey> : ICommand
    {
        Task Update(TEntity item, TKey identity);
    }

    public interface IDelete<TEntity> : ICommand
    {
        Task DeleteBy(TEntity entity);
    }

    public interface IDeleteById<TKey> : ICommand
    {
        Task DeleteBy(TKey identity);
    }

    public interface ICommand<TEntity, TKey> :
        IAdd<TEntity, TKey>,
        IUpdate<TEntity, TKey>,
        IDelete<TEntity>,
        IDeleteById<TKey>
    { }

    [Obsolete("Use IAdd<TEntity, TKey> instead.")]
    public interface IAdd<TEntity> : ICommand
    {
        Task<dynamic> Add(TEntity entity);
    }

    [Obsolete("Use IUpdate<TEntity, TKey> instead.")]
    public interface IUpdate<TEntity> : ICommand
    {
        Task Update(TEntity item, dynamic identity);
    }

    [Obsolete("Use IDeleteById<TKey> instead.")]
    public interface IDeleteById : ICommand
    {
        Task DeleteBy(dynamic identity);
    }

    [Obsolete("Use ICommand<TEntity, TKey> instead.")]
    public interface ICommand<TEntity> :
        IAdd<TEntity>,
        IUpdate<TEntity>,
        IDelete<TEntity>,
        IDeleteById
    { }
}
