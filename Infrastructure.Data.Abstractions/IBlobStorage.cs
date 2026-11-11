using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Data.Abstractions
{
    public interface IDownloadContent
    {
        Task<BinaryData> DownloadContent(string fileName);

        /// <summary>
        /// Downloads the content of a file as <see cref="BinaryData"/>, with cancellation support.
        /// </summary>
        /// <param name="fileName">The name of the file to download.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>The content of the file.</returns>
        /// <remarks>
        /// The default implementation ignores <paramref name="cancellationToken"/> and calls
        /// <see cref="DownloadContent(string)"/>. Override it to support cancellation.
        /// </remarks>
        Task<BinaryData> DownloadContent(string fileName, CancellationToken cancellationToken)
            => DownloadContent(fileName);
    }

    public interface IBlobStorage
    {
        Task Upload<T>(T content, string fileName);
        Task<Stream> Download(string fileName);
        bool Exists(string fileName);

        /// <summary>
        /// Uploads content to a file, with cancellation support.
        /// </summary>
        /// <typeparam name="T">The type of the content.</typeparam>
        /// <param name="content">The content to upload.</param>
        /// <param name="fileName">The name of the file to create or replace.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <remarks>
        /// The default implementation ignores <paramref name="cancellationToken"/> and calls
        /// <see cref="Upload{T}(T, string)"/>. Override it to support cancellation.
        /// </remarks>
        Task Upload<T>(T content, string fileName, CancellationToken cancellationToken)
            => Upload(content, fileName);

        /// <summary>
        /// Downloads a file as a <see cref="Stream"/>, with cancellation support.
        /// </summary>
        /// <param name="fileName">The name of the file to download.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>A stream with the content of the file.</returns>
        /// <remarks>
        /// The default implementation ignores <paramref name="cancellationToken"/> and calls
        /// <see cref="Download(string)"/>. Override it to support cancellation.
        /// </remarks>
        Task<Stream> Download(string fileName, CancellationToken cancellationToken)
            => Download(fileName);

        /// <summary>
        /// Checks asynchronously whether a file exists.
        /// </summary>
        /// <param name="fileName">The name of the file to check.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns><see langword="true"/> if the file exists; otherwise, <see langword="false"/>.</returns>
        /// <remarks>
        /// The default implementation calls <see cref="Exists(string)"/> synchronously and ignores
        /// <paramref name="cancellationToken"/>. Override it with a truly asynchronous implementation.
        /// </remarks>
        Task<bool> ExistsAsync(string fileName, CancellationToken cancellationToken = default)
            => Task.FromResult(Exists(fileName));
    }
}
