using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent (typeof (Equipment))]
public class Pickable : MonoBehaviour {
    public bool AutoPickup = false;
    public int StackCount = 1;
    public AudioClip PickupSound;
    public AudioClip DropSound;
    public UnityEvent OnHighlight, OnDehighlight;

    private Equipment equipment;

    private void Start () {
        equipment = GetComponent<Equipment> ();
    }

    public void Highlight () {
        OnHighlight.Invoke ();
    }

    public void Dehighlight () {
        OnDehighlight.Invoke ();
    }

    public void Pick (int count) {
        if (equipment.IsStackable ()) {
            StackCount -= count;
            if (StackCount <= 0) {
                gameObject.SetActive (false);
            }
        } else {
            gameObject.SetActive (false);
        }
    }

    public void Drop (Transform dropSpawn, int count) {
        transform.position = dropSpawn.position;
        transform.rotation = dropSpawn.rotation;
        if (equipment.IsStackable ()) {
            StackCount = count;
        }
        gameObject.SetActive (true);
    }

    public Equipment GetEquipment () {
        return equipment;
    }
}
