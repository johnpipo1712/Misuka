using System.Runtime.InteropServices;
using Microsoft.Practices.Unity;
using Misuka.Domain.Security;
using Misuka.Infrastructure.Data;
using Misuka.Infrastructure.EntityFramework;
using Misuka.Infrastructure.EntityFramework.Repositories;
using Misuka.Infrastructure.EntityFramework.UnitOfWork;

namespace Misuka.Services
{
  public class ServiceRegister
  {
    private readonly IUnityContainer _container;

    public ServiceRegister(IUnityContainer container)
    {
      _container = container;
    }

    public void Register()
    {
    

      _container.RegisterType<IUserSession, UserSession>();
      _container.RegisterType<ICommandExecutor, CommandExecutor>();
      _container.RegisterType<IUnitOfWork, UnitOfWork>();
    }
  }
}