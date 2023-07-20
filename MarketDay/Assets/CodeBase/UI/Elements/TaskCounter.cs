using System;
using TMPro;
using UnityEngine;

namespace CodeBase.UI.Elements
{
	public class TaskCounter : MonoBehaviour
	{
		public TextMeshProUGUI Counter;
		public TextMeshProUGUI Name;
		private TaskData _taskData;

		public void Construct(TaskData taskData)
		{
			_taskData = taskData;
			Name.text = taskData.Name;
			_taskData.Changed += UpdateCounter;
			UpdateCounter();
		}

		private void UpdateCounter()
		{
			Counter.text = $"{_taskData.Collected}";
		}
	}

}