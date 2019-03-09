using UnityEngine;
using UnityEngine.SceneManagement;

public class GotoMenuTimer : MonoBehaviour
{
	private float _time;
	public  Light dirLight;
	public  float FreezeTime;

	private void Start()
	{
		_time = FreezeTime;
	}

	private void Update()
	{
		if (Global.IsAlive)
			return;

		_time          -= Time.deltaTime;
		dirLight.color =  Color.Lerp(dirLight.color, Color.black, 1.5f * Time.deltaTime);
		if (_time <= 0)
			SceneManager.LoadScene(Scenes.Menu);
	}
}