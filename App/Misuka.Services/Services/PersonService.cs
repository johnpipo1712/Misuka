using Misuka.Domain.Entity;
using Misuka.Infrastructure.EntityFramework.Repositories;
using Misuka.Infrastructure.EntityFramework.Services;

namespace Misuka.Services.Services
{
  public class PersonService : Service<Domain.Entity.Person>, IPersonService
  {
    public PersonService(IRepositoryAsync<Person> repository) : base(repository)
    {
    }
  }
}
