using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickupDisplay : MonoBehaviour {
    public Text Text;
    private string textFormat;

    private void Start () {
        gameObject.SetActive (false);
        textFormat = Text.text;
    }

    public void UpdateHighlight (Pickable pickable) {
        if (pickable) {
            gameObject.SetActive (true);
            Text.text = textFormat
                .Replace ("%key%", "E") // TODO
                .Replace ("%equipment name%", pickable.GetEquipment ().FriendlyName);
        } else {
            gameObject.SetActive (false);
        }
    }
}
