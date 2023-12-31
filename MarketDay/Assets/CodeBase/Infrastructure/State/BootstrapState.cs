using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.StaticData;
using CodeBase.Services;
using CodeBase.Services.Factory;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.Randomizer;
using CodeBase.UI;


namespace CodeBase.Infrastructure.State
{
	public class BootstrapState : IState
	{
		private const string Initial = "Initial";

		private readonly GameStateMachine _stateMachine;
		private readonly SceneLoader _sceneLoader;
		private readonly AllServices _services;

		public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, AllServices services)
		{
			_stateMachine = stateMachine;
			_sceneLoader = sceneLoader;
			_services = services;

			RegisterServices();
		}

		public void Enter() =>
			_sceneLoader.Load(Initial, onLoaded: EnterLoadLevel);

		public void Update() { }

		public void Exit() { }

		private void EnterLoadLevel() =>
			_stateMachine.Enter<LoadProgressState>();

		private void RegisterServices()
		{
			RegisterStaticData();
			_services.RegisterSingle<IGameStateMachine>(_stateMachine);
			_services.RegisterSingle<IAssetProvider>(new AssetProvider());
			_services.RegisterSingle<IRandomService>(new RandomService());
			_services.RegisterSingle<IPersistentProgressService>(new PersistentProgressService());
			_services.RegisterSingle<IGameFactory>(new GameFactory(_services.Single<IAssetProvider>(), _services.Single<IRandomService>(),
				_services.Single<IStaticDataService>(), _services.Single<IPersistentProgressService>()));

			_services.RegisterSingle<IUIFactory>(new UIFactory(_services.Single<IAssetProvider>(), _services.Single<IPersistentProgressService>(), _stateMachine));
		}

		

		private void RegisterStaticData()
		{
			IStaticDataService staticData = new StaticDataService();
			staticData.LoadStaticData();
			_services.RegisterSingle(staticData);
		}
	}

}