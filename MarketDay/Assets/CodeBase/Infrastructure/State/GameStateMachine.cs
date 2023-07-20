using System;
using System.Collections.Generic;
using CodeBase.Infrastructure.StaticData;
using CodeBase.Services;
using CodeBase.Services.Factory;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.Randomizer;
using CodeBase.UI;
using UnityEngine;

namespace CodeBase.Infrastructure.State
{
	public interface IGameStateMachine : IService
	{
		void Enter<TState>() where TState : class, IState;
		void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>;
		void Update();
	}

	public class GameStateMachine : IGameStateMachine
	{
		private readonly Dictionary<Type, IExitableState> _states;
		private IExitableState _activeState;

		public GameStateMachine(SceneLoader sceneLoader, LoadingCurtain loadingCurtain, AllServices allServices)
		{
			_states = new Dictionary<Type, IExitableState>() {
				[typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, allServices),
				[typeof(LoadProgressState)] = new LoadProgressState(this, allServices.Single<IPersistentProgressService>(), allServices.Single<IRandomService>(),
					allServices.Single<IStaticDataService>()),
				[typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader, loadingCurtain, allServices.Single<IGameFactory>(), allServices.Single<IUIFactory>(),
					allServices.Single<IStaticDataService>(), allServices.Single<IPersistentProgressService>(), allServices.Single<IRandomService>()),
				[typeof(GameLoopState)] = new GameLoopState(this, sceneLoader, loadingCurtain, allServices.Single<IRandomService>(),
					allServices.Single<IPersistentProgressService>(), allServices.Single<IUIFactory>(), allServices.Single<IGameFactory>()),
			};
		}

		public void Enter<TState>() where TState : class, IState
		{
			IState state = ChangeState<TState>();
			state.Enter();
		}

		public void Dispose()
		{
			AllServices.Container.Single<IGameFactory>().Dispose();
			AllServices.Container.Single<IUIFactory>().Dispose();
			AllServices.Container.Single<IPersistentProgressService>().Dispose();
		}

		public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
		{
			TState state = ChangeState<TState>();

			state.Enter(payload);
		}

		public void Update()
		{
			_activeState?.Update();
		}

		private TState ChangeState<TState>() where TState : class, IExitableState
		{
			_activeState?.Exit();

			TState state = GetState<TState>();
			_activeState = state;

			return state;
		}

		private TState GetState<TState>() where TState : class, IExitableState =>
			_states[typeof(TState)] as TState;

	}

}