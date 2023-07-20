using CodeBase.UI.Elements;

namespace CodeBase.Services.PersistentProgress
{
  public class PersistentProgressService : IPersistentProgressService
  {
    public TaskData Progress { get; set; }

    public void Dispose()
    {
      Progress = null;
    }
  }
}