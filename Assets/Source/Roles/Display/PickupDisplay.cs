﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickupDisplay : MonoBehaviour {
    public Text Text;
    private string textFormat;

    public Picker picker;

    private void Start () {
        gameObject.SetActive (false);
        textFormat = Text.text;
        picker.OnPickupHighlightChanged.AddListener (UpdateHighlight);
    }

    public void UpdateHighlight (Pickable pickable) {
        if (pickable) {
            gameObject.SetActive (true);
            Equipment equipment = pickable.GetEquipment ();
            Text.text = textFormat
                .Replace ("%key%", ActionCode.Pickup.KeyCode.ToString ())
                .Replace ("%equipment name%", equipment.FriendlyName + (equipment.IsStackable () ? " (" + pickable.StackCount + ")" : ""));
        } else {
            gameObject.SetActive (false);
        }
    }
}
