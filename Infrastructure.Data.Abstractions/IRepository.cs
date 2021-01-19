namespace Infrastructure.Data.Abstractions
{
    public interface IRepository { }
    public interface IRepository<TEntity> :
        IRepository,
        IQuery<TEntity>,
        ICommand<TEntity>
    { }
}
