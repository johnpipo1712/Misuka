using Misuka.Infrastructure.Data;

namespace Misuka.Infrastructure.Data
{
  public interface ICommandExecutor
  {
    T Execute<T>(ICommand<T> cmd);


  }
}