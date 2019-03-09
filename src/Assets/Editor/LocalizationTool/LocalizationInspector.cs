using System.Linq;
using UnityEditor;
using UnityEditorInternal;

namespace UnityEngine
{
	[CustomEditor(typeof(LocalizationSO))]
	public class LocalizationInspector : Editor
	{
		private ReorderableList _langList, _translationList;

		private void Awake()
		{
			var localizationSo           = (LocalizationSO) target;
			var langListProperty         = serializedObject.FindProperty("languages");
			var translationsListProperty = serializedObject.FindProperty("translations");

			//base.OnInspectorGUI();

			_langList = new ReorderableList(serializedObject, langListProperty, true, true, true, true);

			_langList.drawHeaderCallback = rect => EditorGUI.LabelField(rect, "Languages");
			_langList.drawElementCallback = (rect, index, active, focused) =>
				EditorGUI.PropertyField(rect, langListProperty.GetArrayElementAtIndex(index), GUIContent.none);

			_langList.onAddCallback += list =>
			{
				localizationSo.languages.Add("");
				var translations = localizationSo.translations;
				for (var i = 0; i < translations.Count; i++)
					translations[i].Values.Add("");
			};

			_langList.onRemoveCallback = list =>
			{
				var removedAt = list.index;
				localizationSo.languages.RemoveAt(removedAt);

				var translations = localizationSo.translations;
				for (var i = 0; i < translations.Count; i++)
					translations[i].Values.RemoveAt(removedAt + 1);
			};

			//-----------------------------------------------------------
			_translationList = new ReorderableList(serializedObject, translationsListProperty, true, true, true, true);

			_translationList.drawHeaderCallback = rect =>
			{
				var colCount = 2 + localizationSo.languages.Count;
				var colWidth = rect.width / colCount;
				var rects    = new Rect[colCount];
				for (var i = 0; i < colCount; i++)
				{
					var newRect = rect;
					newRect.width = colWidth;
					newRect.x     = colWidth * i + rect.x + 15;

					rects[i] = newRect;
				}

				EditorGUI.LabelField(rects[0], "Key");
				EditorGUI.LabelField(rects[1], "Default");
				for (var i = 0; i < localizationSo.languages.Count; i++)
					EditorGUI.LabelField(rects[i + 2], localizationSo.languages[i]);
			};

			_translationList.drawElementCallback = (rect, index, active, focused) =>
			{
				var colCount = 2 + localizationSo.languages.Count;
				var colWidth = rect.width / colCount;
				var rects    = new Rect[colCount];
				for (var i = 0; i < colCount; i++)
				{
					var newRect = rect;
					newRect.width = colWidth;
					newRect.x     = colWidth * i + rect.x;

					rects[i] = newRect;
				}

				var row = translationsListProperty.GetArrayElementAtIndex(index);

				EditorGUI.PropertyField(rects[0], row.FindPropertyRelative("Key"), GUIContent.none);

				var translationsRow   = row.FindPropertyRelative("Values");
				var translationsCount = localizationSo.translations.First().Values.Count;
				for (var i = 1; i < rects.Length; i++)
				{
					var currRect = rects[i];
					var property = translationsRow.GetArrayElementAtIndex(i - 1);
					EditorGUI.PropertyField(currRect, property, GUIContent.none);
				}
			};
		}

		public override void OnInspectorGUI()
		{
			serializedObject.Update();

			//------------------------------------
			_langList.DoLayoutList();
			_translationList.DoLayoutList();

			//---------------------------
			serializedObject.ApplyModifiedProperties();
		}
	}
}