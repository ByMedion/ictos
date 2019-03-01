using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    private Vector3 _endPoint;
    public float Speed;

    private void Start()
    {
        _endPoint = gameObject.transform.position;
        Global.IsAlive = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown("escape"))
            SceneManager.LoadScene(0);
        
        if (!Global.IsAlive) return;

        if (Input.anyKeyDown)
            if (Input.GetKeyDown("q") || Input.GetKeyDown("[7]"))
                _endPoint = new Vector3(-3f, 2f, 3f);
            else if (Input.GetKeyDown("w") || Input.GetKeyDown("[8]"))
                _endPoint = new Vector3(0f, 2f, 3f);
            else if (Input.GetKeyDown("e") || Input.GetKeyDown("[9]"))
                _endPoint = new Vector3(3f, 2f, 3f);

            else if (Input.GetKeyDown("a") || Input.GetKeyDown("[4]"))
                _endPoint = new Vector3(-3f, 2f, 0f);
            else if (Input.GetKeyDown("s") || Input.GetKeyDown("[5]"))
                _endPoint = new Vector3(0f, 2f, 0f);
            else if (Input.GetKeyDown("d") || Input.GetKeyDown("[6]"))
                _endPoint = new Vector3(3f, 2f, 0f);

            else if (Input.GetKeyDown("z") || Input.GetKeyDown("[1]"))
                _endPoint = new Vector3(-3f, 2f, -3f);
            else if (Input.GetKeyDown("x") || Input.GetKeyDown("[2]"))
                _endPoint = new Vector3(0f, 2f, -3f);
            else if (Input.GetKeyDown("c") || Input.GetKeyDown("[3]"))
                _endPoint = new Vector3(3f, 2f, -3f);
        
        
        gameObject.transform.position =
            Vector3.MoveTowards(gameObject.transform.position, _endPoint, Speed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            Global.IsAlive = false;
            PlayerPrefs.SetInt("BestScore", Global.Record);
            PlayerPrefs.Save();
        }
    }
}