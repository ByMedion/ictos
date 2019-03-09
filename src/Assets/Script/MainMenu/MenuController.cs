using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.MainMenu.Scripts
{
	public class MenuController : MonoBehaviour
	{
		public Pair[] controlPairs;

		private void Awake()
		{
			if (PlayerPrefs.GetInt("IsFirstStart", 1) == 1)
			{
				PlayerPrefs.SetInt("IsFirstStart", 0);
				SceneManager.LoadScene(2);
			}

			Global.Record                                               = PlayerPrefs.GetInt("BestScore", 0);
			GameObject.Find("NumberText").GetComponent<TextMesh>().text = Global.Record.ToString();

			for (var i = 0; i < controlPairs.Length; i++)
			{
				var pair = controlPairs[i];
				if (pair.SceneIndex == -1)
					pair.Control.onDropped.AddListener(() =>
					{
						Application.Quit();
						Debug.Log("App closed");
					});
				else
					pair.Control.onDropped.AddListener(() => SceneManager.LoadScene(pair.SceneIndex));
			}
		}

		private void FixedUpdate()
		{
			if (Input.GetKeyDown(KeyCode.Escape))
				Application.Quit();
		}

		[Serializable]
		public struct Pair
		{
			public int                 SceneIndex;
			public ControlClickHandler Control;
		}
	}
}