using System;

namespace CodeBase.UI.Elements
{
	[Serializable]
	public class TaskData
	{
		public int Collected;
		public string Name;
		public Action Changed;

		public void Collect(int item)
		{
			Collected -= item;
			Changed?.Invoke();
		}
	}
}