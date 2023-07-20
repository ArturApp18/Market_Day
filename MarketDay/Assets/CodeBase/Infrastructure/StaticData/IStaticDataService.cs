using CodeBase.Services;
using CodeBase.StaticData;

namespace CodeBase.Infrastructure.StaticData
{
  public interface IStaticDataService : IService
  {
    void LoadStaticData();
    
    ProductsStaticData ForProducts();
    LevelStaticData ForLevel();
  }


}