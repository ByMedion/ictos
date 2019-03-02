using UnityEngine;
using UnityEngine.Events;

public class ControlClickHandler : MonoBehaviour
{
    public UnityEvent OnDropped;
    private Rigidbody rg;
    
	void Awake () {
        rg = GetComponent<Rigidbody>();
	}
	
	void FixedUpdate () {
        if (!(transform.position.y < -5) && Mathf.Abs(transform.position.z) < 7) return;
        if (OnDropped != null)
            OnDropped.Invoke();
    }

    private void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
            rg.AddForce(Vector3.forward * 300);
        if (Input.GetKeyDown(KeyCode.Mouse1))
            rg.AddForce(Vector3.forward * -300);
    }
}
