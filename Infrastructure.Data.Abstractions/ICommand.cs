using System.Threading.Tasks;

namespace System.Data.Abstractions
{
    public interface ICommand { }

    public interface IAdd<TEntity> : ICommand
    {
        Task<dynamic> Add(TEntity entity);
    }

    public interface IUpdate<TEntity> : ICommand
    {
        Task Update(TEntity item, dynamic identity);
    }

    public interface IDelete<TEntity> : ICommand
    {
        Task DeleteBy(TEntity entity);
    }

    public interface IDeleteById : ICommand
    {
        Task DeleteBy(dynamic identity);
    }

    public interface ICommand<TEntity> :
        IAdd<TEntity>,
        IUpdate<TEntity>,
        IDelete<TEntity>,
        IDeleteById
    {
    }
}
