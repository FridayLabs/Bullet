using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof (Text))]
public class WeaponNameDisplay : MonoBehaviour {

    public Equipper equipper;

    private Text text;
    private string mask = "%equipment%";

    private void Start () {
        text = GetComponent<Text> ();
        mask = text.text;

        equipper.OnChangeActiveSlot.AddListener (delegate (Bag b, int c) { updateUI (); });
        equipper.OnEquip.AddListener (delegate (int c, Equipment e) { updateUI (); });
        equipper.OnDrop.AddListener (delegate (int c, Equipment e) { updateUI (); });

        updateUI ();
    }

    private void updateUI () {
        text.text = mask.Replace ("%equipment%", equipper.GetActiveWeapon ().FriendlyName);
    }
}
