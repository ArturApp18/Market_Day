using System.Collections.Generic;
using CodeBase.Products;
using UnityEngine;

namespace CodeBase.StaticData
{
	[CreateAssetMenu(fileName = "Collected", menuName = "StaticData/Products")]
	public class ProductsStaticData : ScriptableObject
	{
		public List<GameObject> ProductItems;
	}

}