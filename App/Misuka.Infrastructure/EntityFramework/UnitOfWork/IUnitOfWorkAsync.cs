using System.Threading;
using System.Threading.Tasks;
using Misuka.Infrastructure.EntityFramework;
using Misuka.Infrastructure.EntityFramework.Repositories;
using Misuka.Infrastructure.EntityFramework.UnitOfWork;

namespace Misuka.Infrastructure.EntityFramework.UnitOfWork
{
    public interface IUnitOfWorkAsync : IUnitOfWork
    {
        Task<int> SaveChangesAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        IRepositoryAsync<TEntity> RepositoryAsync<TEntity>() where TEntity : class, IObjectState;
    }
}