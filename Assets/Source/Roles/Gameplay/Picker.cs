﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class PickableEvent: UnityEvent<Pickable> {}
public class Picker : MonoBehaviour {

    public PickableEvent OnPickupHighlightChanged;

    private readonly List<GameObject> currentPickables = new List<GameObject> ();
    private GameObject currentClosestPickup;

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

    private void LateUpdate () {
        if (currentPickables.Count <= 0) {
            if (currentClosestPickup) {
                currentClosestPickup.GetComponent<Pickable> ().Dehighlight ();
                currentClosestPickup = null;
                OnPickupHighlightChanged.Invoke(null);
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

        if (currentClosestPickup != highlightenGO) {
            if (currentClosestPickup) {
                currentClosestPickup.GetComponent<Pickable> ().Dehighlight ();
            }
            highlightenGO.GetComponent<Pickable> ().Highlight ();
            currentClosestPickup = highlightenGO;
            OnPickupHighlightChanged.Invoke(highlightenGO.GetComponent<Pickable> ());
        }
    }
}