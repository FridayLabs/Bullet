using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent (typeof (Equipment))]
public class Pickable : MonoBehaviour {
    private Equipment equipment;

    public UnityEvent OnHighlight, OnDehighlight;

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
        if (equipment.IsStackable) {
            equipment.StackCount -= count;
            if (equipment.StackCount <= 0) {
                gameObject.SetActive (false);
            }
        } else {
            gameObject.SetActive (false);
        }
    }

    public void Drop (Transform dropSpawn, int count) {
        transform.position = dropSpawn.position;
        transform.rotation = dropSpawn.rotation;
        if (equipment.IsStackable) {
            equipment.StackCount = count;
        }
        gameObject.SetActive (true);
    }

    public Equipment GetEquipment () {
        return equipment;
    }
}
