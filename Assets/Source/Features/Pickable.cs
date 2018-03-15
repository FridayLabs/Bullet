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

    public void Pick () {
        gameObject.SetActive (false);
    }

    public void Drop (Transform dropSpawn) {
        transform.position = dropSpawn.position;
        transform.rotation = dropSpawn.rotation;
        gameObject.SetActive (true);
    }

    public Equipment GetEquipment () {
        return equipment;
    }
}
