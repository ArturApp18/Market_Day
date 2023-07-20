using UnityEngine;

namespace CodeBase.Products
{
	public class ProductDestroyer : MonoBehaviour
	{
		[SerializeField] private ProductSpawner _productSpawner;

		public void Construct(ProductSpawner productSpawner)
		{
			_productSpawner = productSpawner;
		}
		private void OnTriggerEnter(Collider other)
		{		
			_productSpawner.DestroyProduct(other.gameObject);
		}
	}
}