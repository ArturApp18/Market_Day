using System;
using System.Collections;
using CodeBase.Logic;
using CodeBase.Products;
using CodeBase.UI.Elements;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace CodeBase.Hero
{
	public class PlayerTakeProduct : MonoBehaviour
	{

		[SerializeField] private HeroAnimator _animator;
		[SerializeField] private ProductItem _product;
		[SerializeField] private LookAtProduct _lookAt;
		[SerializeField] private Transform _basket;
		[SerializeField] private Transform _rightHandTarget;
		[SerializeField] private Transform _rightHandTransform;
		[SerializeField] private TwoBoneIKConstraint _constraint;
		[SerializeField] private ShopBasketPopUp _shopBasket;
		[SerializeField] private AnimationCurve _animationCurve;
		[SerializeField] private TriggerObserver _triggerObserver;
		[SerializeField] private TaskData _task;
		[SerializeField] private float _duration;

		private float _expiredTime;
		private Coroutine _moveToBasketCoroutine;

		public void Construct(TaskData progress) =>
			_task = progress;

		private void Start()
		{
			_duration = _animator.GetLength();
			_triggerObserver.TriggerEnter += TriggerEnter;
		}

		private void OnDestroy() =>
			_triggerObserver.TriggerEnter -= TriggerEnter;

		private void TriggerEnter(Collider obj)
		{
			if (_moveToBasketCoroutine == null)
				_moveToBasketCoroutine = StartCoroutine(GrabItem());
		}

		private IEnumerator GrabItem()
		{
			MoveItemToHand();
			yield return new WaitForSeconds(2f);
			MoveItemToBasket();
			_product = null;

		}

		private void FixedUpdate()
		{
			if (_animator.IsTaking)
			{
				if (_product)
					MoveIK(_product.transform);

				float weight = CalculateAxisAnimation(_duration);
				_constraint.weight = _animationCurve.Evaluate(weight);
			}
		}

		public void TakeItem(Transform productItem)
		{
			if (CanTakeItem(productItem))
			{
				RotateToItem(productItem);
				StopMovingItem(productItem);
				_animator.StartTaking();
				
			}
		}

		private bool CanTakeItem(Transform productItem) =>
			!_animator.IsTaking && !_animator.IsWin && productItem.CompareTag(_task.Name);

		public void OnFinishTakeItem()
		{
			_expiredTime = 0;
			StopCoroutine(_moveToBasketCoroutine);
			_moveToBasketCoroutine = null;
		}

		private void MoveItemToHand()
		{
			_product.transform.parent = _rightHandTransform;
			_product.transform.position = _rightHandTransform.position;
		}

		private void MoveIK(Transform productItem) =>
			_rightHandTarget.position = productItem.position;

		private void MoveItemToBasket()
		{
			_shopBasket.PlayPickupFx(_product.transform.position);
			_task.Collect(1);
			_shopBasket.MoveToBasket(_product);
		}

		private void StopMovingItem(Transform productItem)
		{
			_product = productItem.GetComponent<ProductItem>();
			_product.StopMoving();
			
		}

		private void RotateToItem(Transform productItem) =>
			_lookAt.LookAtItem(productItem);

		private float CalculateAxisAnimation(float duration)
		{
			_expiredTime += Time.deltaTime;


			float progress = _expiredTime / duration;
			return progress;
		}

	}
}