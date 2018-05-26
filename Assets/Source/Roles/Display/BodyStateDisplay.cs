using UnityEngine;
using UnityEngine.UI;

public class BodyStateDisplay : MonoBehaviour {

    public Croucher croucher;

    public Text StateText;
    private string mask = "%pos%";

    private void Start () {
        mask = StateText.text;

        croucher.OnCrouch.AddListener (updateUI);
        croucher.OnStandup.AddListener (updateUI);
        updateUI ();
    }

    private void updateUI () {
        StateText.text = mask.Replace ("%pos%", croucher.IsCrouching () ? "Sit" : "Stand");
    }
}
