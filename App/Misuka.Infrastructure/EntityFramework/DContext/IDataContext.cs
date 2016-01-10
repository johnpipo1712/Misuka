using System;

namespace Misuka.Infrastructure.EntityFramework.DContext
{
    public interface IDataContext : IDisposable
    {
        int SaveChanges();
        void SyncObjectState<TEntity>(TEntity entity) where TEntity : class, IObjectState;
        void SyncObjectsStatePostCommit();
        void ExecSQLCommand(string sqlCommand, params object[] parameters);
    }
}