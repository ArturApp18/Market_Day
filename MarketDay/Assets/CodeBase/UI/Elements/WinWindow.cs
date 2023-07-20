using CodeBase.Infrastructure.State;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Elements
{
	public class WinWindow : MonoBehaviour
	{
		public Button RestartLevelButton;
		private IGameStateMachine _stateMachine;

		public void Construct(IGameStateMachine stateMachine) =>
			_stateMachine = stateMachine;

		private void Awake() =>
			RestartLevelButton.onClick.AddListener((RestartLevel));

		private void RestartLevel()
		{
			_stateMachine.Dispose();
			_stateMachine.Enter<LoadProgressState>();
		}
	}
}