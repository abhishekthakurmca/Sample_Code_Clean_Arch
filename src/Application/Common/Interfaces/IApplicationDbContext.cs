﻿using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Anubis.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {

        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        void RollbackTransaction();      

    }
}
