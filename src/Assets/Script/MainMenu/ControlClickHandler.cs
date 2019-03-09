using UnityEngine;
using UnityEngine.Events;

public class ControlClickHandler : MonoBehaviour
{
	public  UnityEvent onDropped;
	private Rigidbody  _rg;

	void Awake()
	{
		_rg = GetComponent<Rigidbody>();
	}

	void FixedUpdate()
	{
		if (!(transform.position.y < -5) && Mathf.Abs(transform.position.z) < 7)
			return;

		if (onDropped != null)
			onDropped.Invoke();
	}

	private void OnMouseOver()
	{
		if (Input.GetKeyDown(KeyCode.Mouse0))
			_rg.AddForce(Vector3.forward * 300);

		if (Input.GetKeyDown(KeyCode.Mouse1))
			_rg.AddForce(Vector3.forward * -300);
	}
}