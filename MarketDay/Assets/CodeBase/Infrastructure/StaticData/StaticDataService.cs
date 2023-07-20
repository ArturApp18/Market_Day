using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Infrastructure.StaticData
{
  public class StaticDataService : IStaticDataService
  {
    private const string ProductsData = "StaticData/Products";
    private const string StaticDataLevel = "StaticData/Level";

    private ProductsStaticData _products;
    private LevelStaticData _levels;


    public void LoadStaticData()
    {
      _products = Resources.Load<ProductsStaticData>(ProductsData);
      _levels = Resources.Load<LevelStaticData>(StaticDataLevel);
    }

    public ProductsStaticData ForProducts() =>
      _products;

    public LevelStaticData ForLevel() =>
      _levels;

    public void Dispose() { }
  }
}