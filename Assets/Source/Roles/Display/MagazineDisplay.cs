using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof (Text))]
public class MagazineDisplay : MonoBehaviour {

    public Equipper equipper;
    public Picker picker;
    public Attacker attacker;
    public Reloader reloader;

    private Text text;
    private string ammoMask = "%1%/%2%";

    private void Start () {
        text = GetComponent<Text> ();
        ammoMask = text.text;

        equipper.OnChangeWeapon.AddListener (updateUI);
        equipper.OnChangeAmmo.AddListener (updateUI);
        attacker.OnAttack.AddListener (delegate (Weapon w) { updateUI (); });
        equipper.OnEquip.AddListener (delegate (int c, Equipment equipment) { updateUI (); });
        reloader.OnReloadIteration.AddListener (delegate (Weapon w) { updateUI (); });
        reloader.OnUnloadIteration.AddListener (delegate (Weapon w) { updateUI (); });

        updateUI ();
    }

    private void updateUI () {
        Weapon weapon = equipper.GetActiveWeapon ();

        string result = ammoMask;

        result = result.Replace ("%1%", weapon.CurrentAmmoCount.ToString ());
        result = result.Replace ("%2%", equipper.GetAmmoCount (equipper.GetActiveAmmo ()).ToString ());

        text.text = result;
    }
}
