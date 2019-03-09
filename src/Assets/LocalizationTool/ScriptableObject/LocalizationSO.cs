using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;

#endif

namespace UnityEngine
{
	public class LocalizationSO : ScriptableObject
	{
		public List<string> languages;

		public List<Translation> translations;

		public LocalizationSO()
		{
			languages    = new List<string> {"Russian"};
			translations = new List<Translation>();
		}

		public void LoadSamples()
		{
			translations = new List<Translation>
			{
				new Translation("app_name", new[] {"Game", "Игра"}),
				new Translation("remove_me", new[] {"remove me", "Удали меня"})
			};
		}
#if UNITY_EDITOR
		[MenuItem("Localization Tools/Create localization")]
		public static void Create()
		{
			var path = EditorUtility.SaveFilePanel("Localization path", "Assets", "Localization.asset", "asset");

			if (path == string.Empty)
				return;

			var localizationSo = CreateInstance<LocalizationSO>();
			AssetDatabase.CreateAsset(localizationSo, path);
			AssetDatabase.SaveAssets();
			AssetDatabase.Refresh();
			EditorUtility.FocusProjectWindow();
			Selection.activeObject = localizationSo;
		}
#endif
	}
}