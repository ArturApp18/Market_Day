using CodeBase.UI.Elements;

namespace CodeBase.Services.PersistentProgress
{
  public interface IPersistentProgressService : IService
  {
    TaskData Progress { get; set; }
  }
}