using UnityEngine;
using UnityEngine.SceneManagement;

public class GotoMenuTimer : MonoBehaviour
{
    public Light dirLight;
    public float FreezeTime;
    private float time;

    private void Start()
    {
        time = FreezeTime;
    }

    private void Update()
    {
        if (Global.IsAlive) return;
        time -= Time.deltaTime;
        dirLight.color = Color.Lerp(dirLight.color, Color.black, 1.5f * Time.deltaTime);
        if (time <= 0)
            SceneManager.LoadScene(0);
    }
}