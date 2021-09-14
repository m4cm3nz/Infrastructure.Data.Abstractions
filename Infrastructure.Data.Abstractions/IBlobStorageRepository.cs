using System.Threading.Tasks;

namespace Infrastructure.Data.Abstractions
{
    public interface IBlobStorageRepository<TEntity>
    {
        void AddQueuedWork(TEntity entity);
        Task<TEntity> GetByKey(string key);
    };
}
