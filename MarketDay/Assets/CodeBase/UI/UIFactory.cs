using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.State;
using CodeBase.Services.PersistentProgress;
using CodeBase.UI.Elements;
using UnityEngine;

namespace CodeBase.UI
{
	public class UIFactory : IUIFactory
	{
		private const string LevelCounterPath = "UI/LevelCounter";
		private readonly IAssetProvider _assets;
		private readonly IPersistentProgressService _progressService;
		private readonly IGameStateMachine _stateMachine;
		private GameObject _hud;
		private GameObject _winWindow;

		public UIFactory(IAssetProvider assets, IPersistentProgressService progressService, IGameStateMachine stateMachine)
		{
			_assets = assets;
			_progressService = progressService;
			_stateMachine = stateMachine;
		}

		public GameObject CreateHUD()
		{
			_hud = _assets.Instantiate(AssetPath.HUDPath);
			_hud.GetComponentInChildren<TaskCounter>().Construct(_progressService.Progress);
			return _hud;
		}

		public void CreateWinWindow()
		{
			_winWindow = _assets.Instantiate(AssetPath.WinWindowPath);
			_winWindow.GetComponent<WinWindow>().Construct(_stateMachine);
		}

		public void Dispose()
		{
			if (_winWindow != null)
				Object.Destroy(_winWindow);

			if (_hud != null)
				Object.Destroy(_hud);
		}
	}

}