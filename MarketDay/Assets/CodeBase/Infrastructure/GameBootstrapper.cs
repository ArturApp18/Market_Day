using System;
using CodeBase.Infrastructure.State;
using UnityEngine;

namespace CodeBase.Infrastructure
{
	public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
	{
		[SerializeField] private LoadingCurtain _curtainPrefab;
		
		private Game _game;

		private void Awake()
		{
			_game = new Game(this, Instantiate(_curtainPrefab));
			_game.StateMachine.Enter<BootstrapState>();

			DontDestroyOnLoad(this);
		}

		private void Update()
		{
			_game.StateMachine.Update();
		}
	}
}