using System.Threading.Tasks;
using CodeBase.Logic;
using CodeBase.Services;
using CodeBase.UI.Elements;
using UnityEngine;

namespace CodeBase.UI
{
	public interface IUIFactory : IService
	{
		GameObject CreateHUD();
		void CreateWinWindow();
	}

}