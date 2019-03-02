using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.MainMenu.Scripts
{
    public class SetScence : MonoBehaviour
    {
        public Pair[] ControlPairs;

        private void Awake()
        {
            if (PlayerPrefs.GetInt("IsFirstStart", 1) == 1)
            {
                PlayerPrefs.SetInt("IsFirstStart", 0);
                SceneManager.LoadScene(2);
            }
            Global.Record = PlayerPrefs.GetInt("BestScore", 0);
            GameObject.Find("NumberText").GetComponent<TextMesh>().text = Global.Record.ToString();

            for (int i = 0; i < ControlPairs.Length; i++)
            {
                var pair = ControlPairs[i];
                if (pair.SceneIndex == -1)
                {
                    pair.Control.OnDropped.AddListener(() => 
                    {
                        Application.Quit();
                        Debug.Log("App closed");
                    });
                }
                else
                    pair.Control.OnDropped.AddListener(() => SceneManager.LoadScene(pair.SceneIndex));
            }
        }

        private void FixedUpdate()
        {
            if (Input.GetKeyDown("escape"))
                Application.Quit();
        }

        [Serializable]
        public struct Pair
        {
            public int SceneIndex;
            public ControlClickHandler Control;
        }
    }
}