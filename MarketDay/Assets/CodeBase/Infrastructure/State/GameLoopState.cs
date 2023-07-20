using CodeBase.Hero;
using CodeBase.Services.Factory;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.Randomizer;
using CodeBase.UI;

namespace CodeBase.Infrastructure.State
{
	public class GameLoopState : IState
	{
		private readonly GameStateMachine _gameStateMachine;
		private readonly SceneLoader _sceneLoader;
		private readonly LoadingCurtain _loadingCurtain;
		private readonly IRandomService _randomService;
		private readonly IPersistentProgressService _progressService;
		private readonly IUIFactory _uiFactory;
		private readonly IGameFactory _factory;
		private bool _isWin = true;

		public GameLoopState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain, IRandomService randomService,
			IPersistentProgressService progressService, IUIFactory uiFactory, IGameFactory factory)
		{
			_gameStateMachine = gameStateMachine;
			_sceneLoader = sceneLoader;
			_loadingCurtain = loadingCurtain;
			_randomService = randomService;
			_progressService = progressService;
			_uiFactory = uiFactory;
			_factory = factory;
		}

		public void Exit() { }

		public void Update()
		{
			if (_progressService.Progress.Collected == 0 && _isWin)
			{
				Win();
			}
		}

		public void Enter()
		{
			_isWin = true;
		}

		private void Win()
		{
			_isWin = false;
			_factory.HeroGameObject.GetComponentInChildren<HeroAnimator>().PlayWin();
			_uiFactory.CreateWinWindow();
		}
	}

}