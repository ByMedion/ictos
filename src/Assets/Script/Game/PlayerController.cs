using System;
using System.Collections;
using Script.Game;
using Script.Game.SwipeDetection;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private static readonly Vector3 TopLeft = new Vector3(-3f, 2f, 3f),
        TopMiddle = new Vector3(0f, 2f, 3f),
        TopRight = new Vector3(3f, 2f, 3f),
        MiddleLeft = new Vector3(-3f, 2f, 0f),
        Middle = new Vector3(0f, 2f, 0f),
        MiddleRight = new Vector3(3f, 2f, 0f),
        BottomLeft = new Vector3(-3f, 2f, -3f),
        BottomMiddle = new Vector3(0f, 2f, -3f),
        BottomRight = new Vector3(3f, 2f, -3f);

    private Vector3 _endPoint;
    private Vector2 _touchStart, _touchEnd;

    private Coroutine movingCoroutine;

    public float speed;
    private SwipeDetector swipeDetector;

    public event Action onGameOver;

    private void Awake()
    {
        _endPoint = gameObject.transform.position;
        Global.IsAlive = true;
        KeyMap.Load();
        swipeDetector = GameManager.instance.swipeDetector;
        swipeDetector.onSwipeDetected += OnSwipeDetected;
    }

    private void OnSwipeDetected(SwipeDirection direction)
    {
        Debug.Log($"Swipe detected: {direction} ({(int) direction} degree)");
        movingCoroutine = StartCoroutine(MovePlayer(direction));
    }

    private IEnumerator MovePlayer(SwipeDirection swipeDirection)
    {
        if (movingCoroutine != null)
            StopCoroutine(movingCoroutine);

        var endPoint = GetEndPoint(swipeDirection);

        var lerpTime = 1;
        var currentLerpTime = Time.deltaTime;
        while (transform.position != endPoint)
        {
            var t = currentLerpTime / lerpTime;
            t = Mathf.Sin(t * Mathf.PI * 0.5f);

            transform.position = Vector3.Lerp(transform.position, endPoint, t);
            currentLerpTime += Time.deltaTime;
            yield return null;
        }
    }

    private static Vector3 GetEndPoint(SwipeDirection swipeDirection)
    {
        switch ((int) swipeDirection)
        {
            case -1: return Middle;
            case 0: return TopLeft;
            case 45: return TopMiddle;
            case 90: return TopRight;
            case 135: return MiddleRight;
            case 180: return BottomRight;
            case 225: return BottomMiddle;
            case 270: return BottomLeft;
            case 315: return MiddleLeft;
            default: throw new ArgumentException();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene(Scenes.Menu);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            StopCoroutine(movingCoroutine);
            onGameOver?.Invoke();
        }
    }
}