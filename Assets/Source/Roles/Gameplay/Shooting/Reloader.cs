using System;
using System.Collections;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent (typeof (Equipper))]
public class Reloader : MonoBehaviour {

    private GameInputManager inputManager;

    private Coroutine reloadingProcess = null;
    private Coroutine unloadingProcess = null;

    [System.Serializable]
    public class ReloadEvent : UnityEvent<Weapon> { }

    public ReloadEvent OnReloadStart, OnReloadIteration, OnReloadFinish, OnUnloadStart, OnUnloadIteration, OnUnloadFinish;

    [SerializeField]
    [ReadOnly]
    private bool isReloading = false;

    [SerializeField]
    [ReadOnly]
    private bool isUnloading = false;

    private Equipper equipper;

    private AudioSource reloadingAudioSource;

    private void Start () {
        inputManager = GameContainer.InputManager ();
        equipper = GetComponent<Equipper> ();
        reloadingAudioSource = gameObject.AddComponent<AudioSource> ();
    }

    private void Update () {
        Weapon weapon = equipper.GetActiveWeapon ();
        Ammo activeAmmo = equipper.GetActiveAmmo ();

        bool canOperate = activeAmmo != null && weapon != null;

        if (canOperate && inputManager.GetKeyDown (ActionCode.Reload)) {
            if (weapon.CurrentAmmoType != null && weapon.CurrentAmmoType != equipper.GetActiveAmmo ()) {
                startUnloading (weapon, delegate {
                    startReloading (weapon, equipper.GetActiveAmmo ());
                });
            } else {
                startReloading (weapon, equipper.GetActiveAmmo ());
            }
        }
        Debug.Log (canOperate);
        if (canOperate && inputManager.GetKeyDown (ActionCode.Unload)) {
            startUnloading (weapon);
        }
    }

    private void startReloading (Weapon weapon, Ammo ammo) {
        if (weapon.CurrentAmmoCount >= weapon.MagazineCount || isReloading) {
            return;
        }

        weapon.CurrentAmmoType = ammo;

        int count = weapon.MagazineCount - weapon.CurrentAmmoCount;
        reloadingProcess = StartCoroutine (weapon.ReloadType.Reload (
            weapon,
            count,
            delegate {
                Debug.Log ("start reloading");
                isReloading = true;
                playSound (weapon.ReloadingStartSound);
                OnReloadStart.Invoke (weapon);
            },
            (int c) => {
                equipper.SetAmmoCount (weapon.CurrentAmmoType, equipper.GetAmmoCount (weapon.CurrentAmmoType) - c);
                weapon.CurrentAmmoCount += c;
                playSound (weapon.ReloadingSound);
                OnReloadIteration.Invoke (weapon);
                return true;
            },
            StopReloading
        ));
    }

    private void startUnloading (Weapon weapon, Action finish = null) {
        if (weapon.CurrentAmmoCount <= 0 || isUnloading) {
            return;
        }
        unloadingProcess = StartCoroutine (weapon.ReloadType.Unload (
            weapon,
            delegate {
                StopReloading ();
                isUnloading = true;
                playSound (weapon.UnloadingStartSound);
                OnUnloadStart.Invoke (weapon);
            },
            (int c) => {
                equipper.SetAmmoCount (weapon.CurrentAmmoType, equipper.GetAmmoCount (weapon.CurrentAmmoType) + c);
                weapon.CurrentAmmoCount -= c;
                playSound (weapon.UnloadingSound);
                OnUnloadIteration.Invoke (weapon);
                return true;
            },
            delegate {
                weapon.CurrentAmmoType = null;
                isUnloading = false;
                if (finish != null) {
                    finish ();
                }
            }
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

    public void StopUnloading () {
        if (isUnloading && unloadingProcess != null) {
            isReloading = false;
            Weapon weapon = equipper.GetActiveWeapon ();
            playSound (weapon.UnloadingFinishSound);
            OnUnloadFinish.Invoke (weapon);
            StopCoroutine (unloadingProcess);
        }
    }

    private void playSound (AudioClip clip) {
        if (clip) {
            reloadingAudioSource.PlayOneShot (clip);
        }
    }
}
