using System;
using System.Collections.Generic;
using System.Linq;

namespace UnityEngine
{
	[Serializable]
	public class Translation
	{
		public string       Key;
		public List<string> Values;

		public Translation()
		{
			Values = new List<string> {""};
		}

		public Translation(string key, IEnumerable<string> values)
		{
			Key    = key;
			Values = values.ToList();
		}

		public Translation Copy()
		{
			return new Translation {Key = Key, Values = new List<string>(Values)};
		}
	}
}