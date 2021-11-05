using System;
using System.IO;
using System.Threading.Tasks;

namespace Infrastructure.Data.Abstractions
{
    public interface IDownloadContent
    {
        Task<BinaryData> DownloadContent(string fileName);
    }

    public interface IBlobStorage
    {
        Task Upload<T>(T content, string fileName);
        Task<Stream> Download(string fileName);
        bool Exists(string fileName);
    }
}
