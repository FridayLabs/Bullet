using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickupDisplay : MonoBehaviour {
    public Text Text;
    private string textFormat;

    public Picker picker;
    public Equipper equipper;

    private void Start () {
        gameObject.SetActive (false);
        textFormat = Text.text;
        picker.OnPickupHighlightChanged.AddListener (UpdateHighlight);
    }

    public void UpdateHighlight (Pickable pickable) {
        if (pickable) {
            Equipment equipment = pickable.GetEquipment ();
            gameObject.SetActive (true);
            if (equipper.CanCarryMoreOf (equipment)) {
                Text.text = textFormat
                    .Replace ("%key%", ActionCode.Pickup.KeyCode.ToString ())
                    .Replace ("%equipment name%", equipment.FriendlyName + (equipment.IsStackable () ? " (" + pickable.StackCount + ")" : ""));
            } else {
                Text.text = "You can't carry more " + equipment.FriendlyName;
            }
        } else {
            gameObject.SetActive (false);
        }
    }
}
