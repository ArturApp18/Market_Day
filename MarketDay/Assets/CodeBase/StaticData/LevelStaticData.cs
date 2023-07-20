using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.StaticData
{
	[CreateAssetMenu(fileName = "LevelData", menuName = "StaticData/Level")]
	public class LevelStaticData : ScriptableObject
	{
		public List<GameObject> ProductItems;
		public int MinValue;
		public int MaxValue;
	}
}