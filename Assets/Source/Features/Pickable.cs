using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Pickable : MonoBehaviour {
    [SerializeField] private Equipment PickupItem;

    public UnityEvent OnHighlight, OnDehighlight;

    public void Highlight() {
        OnHighlight.Invoke();
    }

    public void Dehighlight() {
        OnDehighlight.Invoke();
    }

    public Equipment GetEquipment () {
        return PickupItem;
    }
}
