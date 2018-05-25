using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof (Text))]
public class AttackTypeDisplay : MonoBehaviour {

    public Equipper equipper;
    public Attacker attacker;

    private Text text;
    private string mask = "%attack_type%";

    private void Start () {
        text = GetComponent<Text> ();
        mask = text.text;

        equipper.OnChangeWeapon.AddListener (updateUI);
        attacker.OnChangeAttackType.AddListener (updateUI);

        updateUI ();
    }

    private void updateUI () {

        text.text = mask.Replace ("%attack_type%", equipper.GetActiveWeapon ().GetAttackType ().FriendlyName);
    }
}
