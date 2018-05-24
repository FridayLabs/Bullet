using UnityEngine;

public class AimDisplay : MonoBehaviour {

    public Equipper equipper;

    private void Start () {
        equipper.OnEquip.AddListener (delegate (int p1, Equipment p2) { UpdateCrosshair (); });
        equipper.OnDrop.AddListener (delegate (int p1, Equipment p2) { UpdateCrosshair (); });
        equipper.OnChangeActiveSlot.AddListener (delegate (Bag p1, int p2) { UpdateCrosshair (); });

        UpdateCrosshair ();
    }

    public void UpdateCrosshair () {
        Weapon weapon = equipper.GetActiveWeapon ();
        if (weapon) {
            Cursor.SetCursor (weapon.Crosshair, Vector2.zero, CursorMode.Auto);
        }
    }
}
