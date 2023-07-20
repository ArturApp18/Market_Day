using System;
using CodeBase.Infrastructure.StaticData;
using CodeBase.Logic;
using CodeBase.Services.Factory;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.Randomizer;
using CodeBase.StaticData;
using CodeBase.UI;
using CodeBase.UI.Elements;
using UnityEngine;

namespace CodeBase.Infrastructure.State
{
	public class LoadLevelState : IPayloadedState<string>
	{
		private const string InitialPointTag = "InitialPoint";
		private const string SpawnerTag = "SpawnerPoint";
		private const string DestroyerTag = "DestroyerPoint";

		private readonly GameStateMachine _gameStateMachine;
		private readonly SceneLoader _sceneLoader;
		private readonly LoadingCurtain _loadingCurtain;
		private readonly IGameFactory _gameFactory;
		private readonly IUIFactory _uiFactory;
		private readonly IStaticDataService _dataService;
		private readonly IPersistentProgressService _persistentProgressService;
		private readonly IRandomService _randomService;


		public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain, IGameFactory gameFactory, IUIFactory uiFactory,
			IStaticDataService dataService, IPersistentProgressService persistentProgressService, IRandomService randomService)
		{
			_gameStateMachine = gameStateMachine;
			_sceneLoader = sceneLoader;
			_loadingCurtain = loadingCurtain;
			_gameFactory = gameFactory;
			_uiFactory = uiFactory;
			_dataService = dataService;
			_persistentProgressService = persistentProgressService;
			_randomService = randomService;
		}

		public void Enter(string sceneName)
		{
			_loadingCurtain.Show();
			_sceneLoader.Load(sceneName, OnLoaded);
		}

		public void Update() 
		{ }


		public void Exit() =>
			_loadingCurtain.Hide();

		private void OnLoaded()
		{
			InitGameWorld();

			_gameStateMachine.Enter<GameLoopState>();
		}

		private void InitGameWorld()
		{
			GameObject hero = _gameFactory.CreateHero(GameObject.FindWithTag(InitialPointTag));
			InitSpawner();
			InitDestroyer();
			InitHUD();
		}

		private void InitHUD()
		{
			GameObject hud = _uiFactory.CreateHUD();
			hud.GetComponentInChildren<TaskCounter>().Name.text = _persistentProgressService.Progress.Name;
		}

		private void InitSpawner() =>
			_gameFactory.CreateSpawner(GameObject.FindWithTag(SpawnerTag));

		private void InitDestroyer() =>
			_gameFactory.CreateDestroyer(GameObject.FindWithTag(DestroyerTag));


	}

}