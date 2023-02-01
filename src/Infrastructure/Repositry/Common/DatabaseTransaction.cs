using Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Repositry.Common;

public class DatabaseTransaction : IDatabaseTransaction
{
    private readonly IDbContextTransaction dbContextTransaction;
    private bool disposed = false;

    public DatabaseTransaction(IDbContextTransaction dbContextTransaction)
        => this.dbContextTransaction = dbContextTransaction
        ?? throw new ArgumentNullException(nameof(dbContextTransaction));

    public async Task CommitAsync()
        => await dbContextTransaction.CommitAsync();

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposed)
        {
            return;
        }

        if (disposing)
        {
            dbContextTransaction.Dispose();
        }

        disposed = true;
    }

    ~DatabaseTransaction()
        => Dispose(false);
}