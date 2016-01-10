using System.Threading;
using System.Threading.Tasks;

namespace Misuka.Infrastructure.EntityFramework.DContext
{
    public interface IDataContextAsync : IDataContext
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        Task<int> SaveChangesAsync();
    }
}