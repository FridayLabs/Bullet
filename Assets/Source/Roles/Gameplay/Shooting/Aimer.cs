using UnityEngine;
using UnityEngine.Events;

[RequireComponent (typeof (Equipper))]
public class Aimer : MonoBehaviour {

    [System.Serializable]
    public class WeaponAimEvent : UnityEvent<Weapon> { }

    public WeaponAimEvent OnAimStart, OnAimStop;

    enum AimMode {
        Default,
        Aim
    }
    private AimMode currentAimMode;
    private Equipper equipper;
    private Walker walker;

    private float dispersion = 0f;
    private float lastDispersionChange = 0f;

    public void UpdateCrosshair () {
        Weapon weapon = equipper.GetActiveWeapon ();
        if (weapon) {
            Cursor.SetCursor (weapon.Crosshair, Vector2.zero, CursorMode.Auto);
        }
    }

    public void UpdateDispersion (Weapon weapon) {
        float modifier = currentAimMode == AimMode.Default ? weapon.DispersionPerShoot : weapon.AimDispersionPerShoot;
        if (walker && walker.IsWalking ()) {
            modifier += weapon.DispersionOnMovePerShoot;
        }
        // if (walker && walker.IsWalking ()) { // TODO crouch
        //     modifier += weapon.DispersionOnMovePerShoot;
        // }
        if (weapon.MaximumDispersion > dispersion) {
            changeDispersion (modifier);
        }
    }

    public Vector2 GetAimVector () {
        int sign = Random.Range (-10, 10) > 0 ? 1 : -1;

        Vector2 forward = transform.right.normalized;

        float angle = Mathf.Deg2Rad * sign * Random.Range (0, dispersion);
        Vector2 opposite = transform.up.normalized * forward.magnitude * Mathf.Tan (angle);
        return (forward + opposite).normalized;
    }

    private void Start () {
        equipper = GetComponent<Equipper> ();
        walker = GetComponent<Walker> ();
        changeAimMode (AimMode.Default, true);
        UpdateCrosshair ();
    }

    private void Update () {
        Weapon weapon = equipper.GetActiveWeapon ();
        if (Input.GetMouseButton (1) && weapon) { // TODO
            changeAimMode (AimMode.Aim);
        } else {
            changeAimMode (AimMode.Default);
        }

        if (weapon) {
            restoreDispersion (weapon);
        }
    }

    private void restoreDispersion (Weapon weapon) {
        if (dispersion > 0 && Time.time - lastDispersionChange >= weapon.DispersionRestoreDelay) {
            changeDispersion (-weapon.DispersionRestoreSpeed * Time.deltaTime, false);
        }
        if (dispersion < 0) {
            dispersion = 0;
        }
    }

    private void changeAimMode (AimMode mode, bool force = false) {
        Weapon weapon = equipper.GetActiveWeapon ();
        if (!force && (currentAimMode == mode || weapon == null)) {
            return;
        }
        currentAimMode = mode;
        if (mode == AimMode.Aim) {
            OnAimStart.Invoke (weapon);
        } else {
            OnAimStop.Invoke (weapon);
        }
    }

    private void changeDispersion (float modifier, bool affectTimer = true) {
        dispersion += modifier;
        if (affectTimer) {
            lastDispersionChange = Time.time;
        }
    }
}
