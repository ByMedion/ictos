using System.Linq;
using UnityEditor;

namespace UnityEngine
{
	[CustomEditor(typeof(LocalizationManager))]
	public class LocalizationManagerInspector : Editor
	{
		private SerializedProperty keyProp;
		private string[]           keys;

		private void Awake()
		{
			keyProp = serializedObject.FindProperty("keyIndex");
			keys    = LocalizationManager.source.translations.Select(translation => translation.Key).ToArray();
		}

		public override void OnInspectorGUI()
		{
			LocalizationManager.source = (LocalizationSO) EditorGUILayout.ObjectField("Source",
				LocalizationManager.source,
				typeof(LocalizationSO),
				false);

			keyProp.intValue = EditorGUILayout.Popup("Key", keyProp.intValue, keys);
		}
	}
}