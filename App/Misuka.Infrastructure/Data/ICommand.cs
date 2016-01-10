
namespace Misuka.Infrastructure.Data
{
  public interface ICommand<T>
  {
    T Execute();
  }
}
