using CodeBase.Hero;
using UnityEngine;

namespace CodeBase.Products
{
	public class ProductItem : MonoBehaviour
	{
		[SerializeField] private float _speed;
		[SerializeField] private GameObject _popUp;
		[SerializeField] private PlayerTakeProduct _playerTakeProduct;

		private bool _isMoving = true;


		public void Construct(PlayerTakeProduct playerTakeProduct) =>
			_playerTakeProduct = playerTakeProduct;

		private void Update()
		{
			if (_isMoving)
			{
				Vector3 lineDirection = Vector3.left * _speed * Time.deltaTime;
				transform.Translate(lineDirection);
			}
		}

		public void StopMoving() =>
			_isMoving = false;


		public void ShowText()
		{
			_popUp.transform.eulerAngles = Vector3.zero;
			_popUp.SetActive(true);
		}

		private void OnMouseDown() =>
			_playerTakeProduct.TakeItem(transform);

	}

}