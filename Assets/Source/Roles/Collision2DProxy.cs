using UnityEngine;
using UnityEngine.Events;


[System.Serializable]
public class ProxyEvent : UnityEvent<Collider2D> {
}

[RequireComponent(typeof(Collider2D))]
public class Collision2DProxy : MonoBehaviour {

    public ProxyEvent OnTriggerEnter, OnTriggerExit;
    
    private void OnTriggerEnter2D(Collider2D other) {
        OnTriggerEnter.Invoke(other);
    }

    private void OnTriggerExit2D(Collider2D other) {
        OnTriggerExit.Invoke(other);
    }
}