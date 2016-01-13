using Misuka.Infrastructure.EntityFramework.Repositories;
using Misuka.Infrastructure.EntityFramework.Services;

namespace Misuka.Services.Services
{
  public class RoleService : Service<Domain.Entity.Role>, IRoleService
  {
    public RoleService(IRepositoryAsync<Domain.Entity.Role> repository)
      : base(repository)
    {
    }
  }
}
