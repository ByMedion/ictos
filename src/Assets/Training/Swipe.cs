using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Swipe : MonoBehaviour
{
    private GameObject _next;
    private Text _txt;

    public int NextNumber;

    private void Start()
    {
        _txt = GameObject.Find("ProgressText").GetComponent<Text>();
        switch (name)
        {
            case "Zero":
                _next = GameObject.Find("First");
                break;
            case "First":
                _next = GameObject.Find("Second");
                break;
            case "Second":
                _next = GameObject.Find("Third");
                break;
            case "Third":
                _next = GameObject.Find("Quad");
                break;
            case "Quad":
                _next = GameObject.Find("Five");
                break;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown("escape"))
            SceneManager.LoadScene(0);

        if (name == "Zero")
            transform.position = Vector3.MoveTowards(transform.position,
                transform.position + Vector3.left * transform.position.x,
                30 * Time.deltaTime);

        if (!(transform.position.x < -14)) return;

        Destroy(gameObject, 0.5f);
        _txt.text = NextNumber + "/5";
        if (_next != null)
            _next.transform.position = Vector3.MoveTowards(_next.transform.position,
                _next.transform.position + Vector3.left * _next.transform.position.x,
                30 * Time.deltaTime);
        else if (_next == null)
            SceneManager.LoadScene(1);
    }

    private void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
            GetComponent<Rigidbody>().AddForce(Vector3.left * 3500);
    }
}