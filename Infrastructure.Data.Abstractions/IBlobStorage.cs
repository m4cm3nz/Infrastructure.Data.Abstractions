﻿using System.IO;
using System.Threading.Tasks;

namespace Infrastructure.Data.Abstractions
{
    public interface IBlobStorage
    {
        Task Upload<T>(T content, string fileName);
        Task<Stream> Download(string fileName);
        bool Exists(string fileName);
    }
}
