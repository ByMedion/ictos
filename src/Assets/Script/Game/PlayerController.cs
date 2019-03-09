using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
	private static readonly Vector3 TopLeft      = new Vector3(-3f, 2f, 3f),
	                                TopMiddle    = new Vector3(0f, 2f, 3f),
	                                TopRight     = new Vector3(3f, 2f, 3f),
	                                MiddleLeft   = new Vector3(-3f, 2f, 0f),
	                                Middle       = new Vector3(0f, 2f, 0f),
	                                MiddleRight  = new Vector3(3f, 2f, 0f),
	                                BottomLeft   = new Vector3(-3f, 2f, -3f),
	                                BottomMiddle = new Vector3(0f, 2f, -3f),
	                                BottomRight  = new Vector3(3f, 2f, -3f);

	private Vector3 _endPoint;
#if UNITY_ANDROID || UNITY_IOS
	private Vector2 _touchStart, _touchEnd;
#endif
	public float speed;

	private void Awake()
	{
		_endPoint      = gameObject.transform.position;
		Global.IsAlive = true;
		KeyMap.Load();
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
			SceneManager.LoadScene(Scenes.Menu);

		if (!Global.IsAlive)
			return;

#if UNITY_STANDALONE
		if (Input.anyKeyDown)
			if (Input.GetKeyDown(KeyMap.TopLeft))
				_endPoint = TopLeft;
			else if (Input.GetKeyDown(KeyMap.TopMiddle))
				_endPoint = TopMiddle;
			else if (Input.GetKeyDown(KeyMap.TopRight))
				_endPoint = TopRight;

			else if (Input.GetKeyDown(KeyMap.MiddleLeft))
				_endPoint = MiddleLeft;
			else if (Input.GetKeyDown(KeyMap.Middle))
				_endPoint = Middle;
			else if (Input.GetKeyDown(KeyMap.MiddleRight))
				_endPoint = MiddleRight;

			else if (Input.GetKeyDown(KeyMap.BottomLeft))
				_endPoint = BottomLeft;
			else if (Input.GetKeyDown(KeyMap.BottomMiddle))
				_endPoint = BottomMiddle;
			else if (Input.GetKeyDown(KeyMap.BottomRight))
				_endPoint = BottomRight;
#elif UNITY_ANDROID

#if UNITY_EDITOR
		if (Input.GetMouseButtonDown(0))
		{
			_touchStart = Input.mousePosition;
		}
		else if (Input.GetMouseButtonUp(0))
		{
			_touchEnd = Input.mousePosition;
#else
		var touch = Input.touches[0];
		if (touch.phase == TouchPhase.Began)
			touchStart = touch.position;
		else if (touch.phase == TouchPhase.Ended)
		{
			touchEnd = touch.position;
#endif

			var delta = _touchEnd - _touchStart;
			if (Math.Abs(delta.magnitude) < 0.05f)
			{
				_endPoint = Middle;
			}
			else
			{
				var swipeAngle = Rounded45Angle(delta);
				switch (swipeAngle)
				{
					case 360:
					case 0:
						_endPoint = TopLeft;

						break;
					case 45:
						_endPoint = TopMiddle;

						break;
					case 90:
						_endPoint = TopRight;

						break;
					case 135:
						_endPoint = MiddleRight;

						break;
					case 180:
						_endPoint = BottomRight;

						break;
					case 225:
						_endPoint = BottomMiddle;

						break;
					case 270:

						_endPoint = BottomLeft;

						break;
					case 315:
						_endPoint = MiddleLeft;

						break;
				}
			}
		}
#endif
		gameObject.transform.position =
			Vector3.MoveTowards(gameObject.transform.position, _endPoint, speed * Time.deltaTime);
	}
#if UNITY_ANDROID
	private static int Rounded45Angle(Vector2 vector)
	{
		float angle;
		if (vector.x < 0)
			angle = 360 - Mathf.Atan2(vector.x, vector.y) * Mathf.Rad2Deg * -1;
		else
			angle = Mathf.Atan2(vector.x, vector.y) * Mathf.Rad2Deg;

		return Mathf.RoundToInt(angle / 45) * 45;
	}
#endif

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("Wall"))
		{
			Global.IsAlive = false;
			PlayerPrefs.SetInt("BestScore", Global.Record);
			PlayerPrefs.Save();
		}
	}
}