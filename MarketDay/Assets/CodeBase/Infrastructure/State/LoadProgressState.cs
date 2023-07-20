using CodeBase.Infrastructure.StaticData;
using CodeBase.Logic;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.Randomizer;
using CodeBase.StaticData;
using CodeBase.UI.Elements;
using UnityEngine;

namespace CodeBase.Infrastructure.State
{
	public class LoadProgressState : IState
	{
		private const string Main = "Main";

		private readonly GameStateMachine _gameStateMachine;
		private readonly IPersistentProgressService _progressService;
		private readonly IRandomService _randomService;
		private readonly IStaticDataService _staticData;

		public LoadProgressState(GameStateMachine gameStateMachine, IPersistentProgressService progressService, IRandomService randomService, IStaticDataService staticData)
		{
			_gameStateMachine = gameStateMachine;
			_progressService = progressService;
			_randomService = randomService;
			_staticData = staticData;
		}

		public void Enter()
		{
			InitNewProgress();

			_gameStateMachine.Enter<LoadLevelState, string>(Main);
		}

		public void Exit() { }

		public void Update()
		{
			
		}

		private void InitNewProgress()
		{
			_progressService.Progress = NewProgress();
		}

		private TaskData NewProgress()
		{
			TaskData taskData = new TaskData();
			LevelStaticData levelData = _staticData.ForLevel();
			taskData.Collected = _randomService.Next(levelData.MinValue,levelData.MaxValue);
			taskData.Name = levelData.ProductItems[_randomService.Next(0, 3)].name;

			return taskData;
		}
	}
}