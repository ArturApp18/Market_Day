using System;
using UnityEngine;

namespace CodeBase.Hero
{
	public class LookAtProduct : MonoBehaviour
	{
		public float Speed;

		private Transform _productTransform;
		private Vector3 _basePosition;
		private Vector3 _positionToLook;

		private void Start()
		{
			_basePosition = transform.position;
		}

		private void Update()
		{
			if (_productTransform)
				RotateTowardsProducts();
		}
		
		public void LookAtItem(Transform product)
		{
			_productTransform = product;
			Vector3 positionDelta = _productTransform.position - transform.position;
			_positionToLook = new Vector3(positionDelta.x, transform.position.y, positionDelta.z);
		}

		private void RotateTowardsProducts()
		{
			transform.rotation = SmoothedRotation(transform.rotation, _positionToLook);
		}
		
    
		private Quaternion SmoothedRotation(Quaternion rotation, Vector3 positionToLook) =>
			Quaternion.Lerp(rotation, TargetRotation(positionToLook), SpeedFactor());

		private Quaternion TargetRotation(Vector3 position) =>
			Quaternion.LookRotation(position);

		private float SpeedFactor() =>
			Speed * Time.deltaTime;
	}
}
