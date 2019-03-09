using UnityEngine;
using UnityEngine.Events;

public class SlideController : MonoBehaviour
{
	public UnityEvent<Transform> onClick;

	private void Awake()
	{
		onClick = new ParamEvent<Transform>();
	}

	private void OnMouseDown()
	{
		if (onClick != null)
			onClick.Invoke(transform);
	}

	private class ParamEvent<T> : UnityEvent<T>
	{
	}
}