using System;
using CodeBase.CameraLogic;
using CodeBase.Hero;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.StaticData;
using CodeBase.Products;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.Randomizer;
using CodeBase.StaticData;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CodeBase.Services.Factory
{
	public class GameFactory : IGameFactory
	{
		

		private readonly IAssetProvider _assetProvider;
		private readonly IRandomService _randomService;
		private readonly IStaticDataService _staticDataService;
		private readonly IPersistentProgressService _persistentProgressService;

		private ProductSpawner _productSpawner;

		public GameObject HeroGameObject { get; set; }
		private PlayerTakeProduct playerTakeProduct;
		private GameObject _productDestroyer;

		public GameFactory(IAssetProvider assetProvider, IRandomService randomService, IStaticDataService staticDataService,
			IPersistentProgressService persistentProgressService)
		{
			_assetProvider = assetProvider;
			_randomService = randomService;
			_staticDataService = staticDataService;
			_persistentProgressService = persistentProgressService;
		}


		public GameObject CreateHero(GameObject at)
		{
			HeroGameObject = _assetProvider.Instantiate(AssetPath.HeroPath, at.transform.position); 
			playerTakeProduct = HeroGameObject.GetComponentInChildren<PlayerTakeProduct>();
			playerTakeProduct.Construct(_persistentProgressService.Progress);
			CameraFollow();
			return HeroGameObject;
			
		}

		public void CreateSpawner(GameObject at)
		{
			ProductSpawner productSpawner =  _assetProvider.Instantiate(AssetPath.ProductsSpawner, at.transform.position).GetComponent<ProductSpawner>();
			_productSpawner = productSpawner;
			productSpawner.Construct(this, _randomService);
		}
		
		public void CreateDestroyer(GameObject at)
		{
			ProductDestroyer productDestroyer =  _assetProvider.Instantiate(AssetPath.ProductsDestroyer, at.transform.position).GetComponent<ProductDestroyer>();
			productDestroyer.Construct(_productSpawner);
		}

		public void Dispose()
		{
			if (HeroGameObject != null)
				Object.Destroy(HeroGameObject);

			if (_productSpawner != null)
				Object.Destroy(_productSpawner.gameObject);

			if (_productDestroyer != null)
				Object.Destroy(_productDestroyer.gameObject);
		}

		private void CameraFollow() =>
			Camera.main.GetComponent<CameraMoveToHero>()
				.Construct(HeroGameObject.GetComponentInChildren<HeroAnimator>());

		public GameObject CreateProduct(Transform parent)
		{
			ProductsStaticData productsData = _staticDataService.ForProducts();

			GameObject productItem = Object.Instantiate(productsData.ProductItems[_randomService.Next(0, 3)], parent);

			productItem.GetComponent<ProductItem>().Construct(playerTakeProduct);
			return productItem;
		}
		
	}
}