using System.Threading.Tasks;

namespace Infrastructure.Data.Abstractions
{
    public interface IBlobStorageRepository<TEntity> where TEntity: IKeyable
    {
        void AddQueuedWork(TEntity entity);
        Task<TEntity> GetByKey(string key);
    };
}
