using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent (typeof (AudioSource))]
public class Picker : MonoBehaviour {

    [System.Serializable]
    public class PickableEvent : UnityEvent<Pickable> { }

    public Transform DropSpawn;

    public PickableEvent OnPickupHighlightChanged, OnPickup, OnDrop;

    private readonly List<GameObject> currentPickables = new List<GameObject> ();
    private GameObject currentClosestPickup;

    private AudioSource audioSource;

    private void Start () {
        audioSource = GetComponent<AudioSource> ();
    }

    public void OnPickupEnter (Collider2D other) {
        Pickable pickable = other.GetComponent<Pickable> ();
        if (pickable) {
            currentPickables.Add (other.gameObject);
        }
    }

    public void OnPickupExit (Collider2D other) {
        Pickable pickable = other.GetComponent<Pickable> ();
        if (pickable) {
            currentPickables.Remove (other.gameObject);
        }
    }
    public Pickable GetHighlightedPickable () {
        return currentClosestPickup ? currentClosestPickup.GetComponent<Pickable> () : null;
    }

    public void Pick (Pickable pickable, int count) {
        pickable.Pick (count);
        if (!pickable.CanBePicked ()) {
            currentPickables.Remove (pickable.gameObject);
        }
        audioSource.PlayOneShot (pickable.PickupSound);
        updateHighlight (true);
        OnPickup.Invoke (pickable);
    }

    public void Drop (Pickable pickable, int count) {
        pickable.Drop (DropSpawn, count);
        if (pickable.CanBePicked ()) {
            currentPickables.Add (pickable.gameObject);
        }
        audioSource.PlayOneShot (pickable.DropSound);
        updateHighlight (true);
        OnDrop.Invoke (pickable);
    }

    private void LateUpdate () {
        updateHighlight (false);
    }

    private void updateHighlight (bool force) {
        if (currentPickables.Count <= 0) {
            if (currentClosestPickup) {
                currentClosestPickup.GetComponent<Pickable> ().Dehighlight ();
                currentClosestPickup = null;
                OnPickupHighlightChanged.Invoke (null);
            }
            return;
        }

        float distance = -1f;
        GameObject highlightenGO = null;

        foreach (var pickup in currentPickables) {
            float currentDistance = (pickup.transform.position - transform.position).magnitude;
            if (distance < 0 || currentDistance < distance) {
                highlightenGO = pickup;
                distance = currentDistance;
            }
        }

        if (force || currentClosestPickup != highlightenGO) {
            if (currentClosestPickup) {
                currentClosestPickup.GetComponent<Pickable> ().Dehighlight ();
            }
            highlightenGO.GetComponent<Pickable> ().Highlight ();
            currentClosestPickup = highlightenGO;
            OnPickupHighlightChanged.Invoke (highlightenGO.GetComponent<Pickable> ());
        }
    }
}
