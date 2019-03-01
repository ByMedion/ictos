using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.MainMenu.Scripts
{
    public class SetScence : MonoBehaviour
    {
        private Rigidbody rg;
        public int SceneIndex;

        private void Start()
        {
            if (PlayerPrefs.GetInt("IsFirstStart", 1) == 1)
            {
                PlayerPrefs.SetInt("IsFirstStart", 0);
                SceneManager.LoadScene(2);
            }
            Global.Record = PlayerPrefs.GetInt("BestScore", 0);
            GameObject.Find("NumberText").GetComponent<TextMesh>().text = Global.Record.ToString();

            rg = GetComponent<Rigidbody>();
        }

        private void OnMouseOver()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
                rg.AddForce(Vector3.forward * 300);
            if (Input.GetKeyDown(KeyCode.Mouse1))
                rg.AddForce(Vector3.forward * -300);
        }

        private void FixedUpdate()
        {
            if (Input.GetKeyDown("escape") || SceneIndex < 0)
                Application.Quit();

            if (!(transform.position.y < -5) && Mathf.Abs(transform.position.z) < 7) return;

            SceneManager.LoadScene(SceneIndex);
        }
    }
}