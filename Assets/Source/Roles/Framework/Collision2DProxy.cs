using UnityEngine;
using UnityEngine.Events;

[RequireComponent (typeof (Collider2D))]
public class Collision2DProxy : MonoBehaviour {

    [System.Serializable]
    public class ProxyEvent : UnityEvent<Collider2D> { }

    public ProxyEvent OnTriggerEnter, OnTriggerExit;

    private void OnTriggerEnter2D (Collider2D other) {
        OnTriggerEnter.Invoke (other);
    }

    private void OnTriggerExit2D (Collider2D other) {
        OnTriggerExit.Invoke (other);
    }
}
