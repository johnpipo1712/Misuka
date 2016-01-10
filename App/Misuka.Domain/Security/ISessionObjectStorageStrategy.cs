namespace Misuka.Domain.Security
{
  public interface ISessionObjectStorageStrategy
  { 
    void Save(SessionData sessionData);
    SessionDataLoadingResult Load();
    void Remove();
  }
}
