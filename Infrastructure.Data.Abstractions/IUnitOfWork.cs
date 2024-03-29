﻿using System;
using System.Data;
using System.Threading.Tasks;

namespace Infrastructure.Data.Abstractions
{
    public interface IUnitOfWork : IDisposable
    {
        Guid Id { get; }
        Task<IDbConnection> GetOpenedConnectionAsync();
        IDbTransaction Transaction { get; }
        void Begin();
        void Commit();
        void Rollback();
    }
}
