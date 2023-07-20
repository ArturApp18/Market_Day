using UnityEngine;

namespace CodeBase.Products
{
	public class ShopBasketPopUp : MonoBehaviour
	{
		[SerializeField] private GameObject _pickupFxPrefab;

		public void MoveToBasket(ProductItem productItem)
		{
			productItem.transform.parent = transform;
			productItem.transform.position = transform.position;
			productItem.transform.localEulerAngles = Vector3.zero;
			productItem.ShowText();
		}
		public void PlayPickupFx(Vector3 productPosition) =>
			Instantiate(_pickupFxPrefab, productPosition, Quaternion.identity);

		
	}
}