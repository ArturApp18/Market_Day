using System;
using CodeBase.Hero;
using UnityEngine;

namespace CodeBase.CameraLogic
{
	public class CameraMoveToHero : MonoBehaviour
	{
		public Transform SecondPlace;
		public Transform FirstPlace;
		public float SmoothSpeed = 0.125f;

		private Vector3 offset;
		private float _tresHold = 2f;
		private HeroAnimator _heroAnimator;
		private Transform _startPosition;

		public void Construct(HeroAnimator heroAnimator)
		{
			transform.position = FirstPlace.position; 
			_heroAnimator = heroAnimator;
		}

		private void Start()
		{
			offset = transform.position + SecondPlace.position;
		}
		

		private void LateUpdate()
		{
			if (Vector3.Distance(transform.position, SecondPlace.position) > _tresHold && _heroAnimator.IsWin)
			{
				Vector3 desiredPosition = SecondPlace.position - offset;

				Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, SmoothSpeed * Time.deltaTime);
				transform.position = smoothedPosition;
			}
		}

	}
}