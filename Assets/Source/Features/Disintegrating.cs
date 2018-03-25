using UnityEngine;
using UnityEngine.Events;

public class Disintegrating : MonoBehaviour {

    [System.Serializable]
    public class DisintegratingEvent : UnityEvent<Vector2, GameObject> { }

    public DisintegratingEvent OnDisintegrated;

    private float DisintegratingDistance;

    private Vector2 startPosition;

    public void SetDisintegratingDistance (float distance) {
        DisintegratingDistance = distance;
    }

    private void OnEnable () {
        startPosition = transform.position;
    }

    private void Update () {
        if (Vector2.Distance (startPosition, transform.position) >= DisintegratingDistance) {
            OnDisintegrated.Invoke (transform.position, gameObject);
            gameObject.SetActive (false);
        }
    }
}
