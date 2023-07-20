using CodeBase.Infrastructure.State;
using CodeBase.Services;

namespace CodeBase.Infrastructure
{
	public class Game
	{
		public readonly GameStateMachine StateMachine;

		public Game(ICoroutineRunner coroutineRunner, LoadingCurtain loadingCurtain)
		{
			StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), loadingCurtain, AllServices.Container);
		}
	}

}