﻿using NaughtyAttributes;
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

    [SerializeField]
    [ReadOnly]
    private AimMode currentAimMode;

    private Equipper equipper;
    private Walker walker;
    private Croucher croucher;

    [ReadOnly]
    [SerializeField]
    private float spread = 0f;
    private float lastSpreadChange = 0f;

    public void UpdateSpread (Weapon weapon) {
        float modifier = currentAimMode == AimMode.Default ? weapon.SpreadPerShot : weapon.AimSpreadPerShot;
        if (walker && walker.IsWalking ()) {
            modifier += weapon.SpreadOnMovePerShot;
        }
        if (croucher && croucher.IsCrouching ()) {
            modifier += weapon.SpreadOnSeatPerShot;
        }
        if (weapon.MaximumSpread > spread) {
            changeSpread (modifier);
        }
    }

    public Vector2 GetAimVector () {
        int sign = Random.Range (-10, 10) > 0 ? 1 : -1;

        Vector2 forward = transform.right.normalized * 20;

        float angle = Mathf.Deg2Rad * sign * Random.Range (0, spread / 2);
        Vector2 opposite = transform.up.normalized * forward.magnitude * Mathf.Tan (angle);
        return (forward + opposite).normalized;
    }

    private void Start () {
        equipper = GetComponent<Equipper> ();
        walker = GetComponent<Walker> ();
        croucher = GetComponent<Croucher> ();
        changeAimMode (AimMode.Default, true);
        ResetSpread ();
    }

    private void Update () {
        Weapon weapon = equipper.GetActiveWeapon ();
        if (GameContainer.InputManager ().GetKey (ActionCode.Aim) && weapon) { // TODO
            changeAimMode (AimMode.Aim);
        } else {
            changeAimMode (AimMode.Default);
        }

        if (weapon) {
            restoreSpread (weapon);
        }
    }

    public void ResetSpread () {
        Weapon weapon = equipper.GetActiveWeapon ();
        spread = weapon ? weapon.DefaultSpread : 0;
    }

    private void restoreSpread (Weapon weapon) {
        float defaultSpread = weapon.DefaultSpread;
        if (spread > defaultSpread && Time.time - lastSpreadChange >= weapon.SpreadRestoreDelay) {
            changeSpread (-weapon.SpreadRestoreSpeed * Time.deltaTime, false);
        }
        if (spread < defaultSpread) {
            spread = defaultSpread;
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

    private void changeSpread (float modifier, bool affectTimer = true) {
        spread += modifier;
        if (affectTimer) {
            lastSpreadChange = Time.time;
        }
    }
}
