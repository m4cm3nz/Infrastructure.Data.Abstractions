using System.Threading.Tasks;

namespace Infrastructure.Data.Abstractions
{
    public interface ICacheProvider
    {
        Task<T> Get<T>(string key);
        Task Add<T>(T @object, string key);
        Task Remove(string key);
        Task Invalidate(string key);
    }
}
