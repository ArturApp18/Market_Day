using System;
using System.Collections;
using System.Collections.Generic;
using CodeBase.Services.Factory;
using CodeBase.Services.Randomizer;
using UnityEngine;

namespace CodeBase.Products
{
	public class ProductSpawner : MonoBehaviour
	{
		[SerializeField] private List<GameObject> _productItems;
		[SerializeField] private List<GameObject> _productsOnScene;

		[SerializeField] private float DelayBeetwenSpawn;

		private IGameFactory _factory;
		private IRandomService _randomService;

		private int _maxItemOnLine = 3;
		private Coroutine _spawnCoroutine;
		private bool _readyToSpawn = true;

		public void Construct(IGameFactory gameFactory, IRandomService randomService)
		{
			_factory = gameFactory;
			_randomService = randomService;
		}

		private void Update()
		{
			if (_readyToSpawn)
			{
				StartCoroutine(Spawn());
			}
		}

		public void DestroyProduct(GameObject product)
		{
			_productsOnScene.Remove(product);
			Destroy(product);
		}

		private IEnumerator Spawn()
		{
			_readyToSpawn = false;
			GameObject product = _factory.CreateProduct(transform);
			_productsOnScene.Add(product);
			yield return new WaitForSeconds(DelayBeetwenSpawn);
			_readyToSpawn = true;
		}


	}

	public enum ProductTypeId
	{
		Carrot,
		Donut,
		Apple,
	}
}