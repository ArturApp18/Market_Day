using CodeBase.Products;
using UnityEngine;

namespace CodeBase.Services.Factory
{
	public interface IGameFactory : IService
	{
		GameObject CreateHero(GameObject at);
		GameObject HeroGameObject { get; set; }
		GameObject CreateProduct(Transform parent);
		void CreateSpawner(GameObject at);
		void CreateDestroyer(GameObject at);
		void Dispose();
		

	}

}