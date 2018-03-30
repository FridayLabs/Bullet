using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent (typeof (Equipper))]
public class Reloader : MonoBehaviour {

    private GameInputManager inputManager;

    private Coroutine reloadingProcess = null;

    [System.Serializable]
    public class ReloadEvent : UnityEvent<Weapon> { }

    public ReloadEvent OnReloadStart, OnReloadIteration, OnReloadFinish;

    [SerializeField]
    [ReadOnly]
    private bool isReloading = false;

    private Equipper equipper;

    private void Start () {
        inputManager = GameContainer.InputManager ();
        equipper = GetComponent<Equipper> ();
    }

    private void Update () {
        if (inputManager.GetKeyDown (ActionCode.Reload)) {
            if (!isReloading) {
                startReloading ();
            }
        }
    }

    private void startReloading () {
        Weapon weapon = equipper.GetActiveWeapon ();
        Ammo activeAmmo = equipper.GetActiveAmmo ();
        if (activeAmmo == null || weapon == null || weapon.CurrentAmmoCount >= weapon.MagazineCount) {
            return;
        }
        if (weapon.CurrentAmmoType != null && weapon.CurrentAmmoType != activeAmmo) {
            Debug.Log ("Unloading");
        }

        weapon.CurrentAmmoType = activeAmmo;

        int count = weapon.MagazineCount - weapon.CurrentAmmoCount;
        reloadingProcess = StartCoroutine (weapon.ReloadType.Reload (
            weapon,
            count,
            delegate {
                isReloading = true;
                playSound (weapon.ReloadingStartSound);
                OnReloadStart.Invoke (weapon);
            },
            (int c) => {
                equipper.SetActiveAmmoCount (equipper.GetActiveAmmoCount () - c);
                weapon.CurrentAmmoCount += c;
                playSound (weapon.ReloadingSound);
                OnReloadIteration.Invoke (weapon);
                return true;
            },
            StopReloading
        ));
    }

    public void StopReloading () {
        if (isReloading && reloadingProcess != null) {
            isReloading = false;
            Weapon weapon = equipper.GetActiveWeapon ();
            playSound (weapon.ReloadingFinishSound);
            OnReloadFinish.Invoke (weapon);
            StopCoroutine (reloadingProcess);
        }
    }

    private void playSound (AudioClip clip) {
        if (!clip) {
            return;
        }
        AudioSource.PlayClipAtPoint (clip, transform.position);
    }
}
