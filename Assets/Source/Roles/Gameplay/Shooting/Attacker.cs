using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent (typeof (Equipper))]
[RequireComponent (typeof (Aimer))]
public class Attacker : MonoBehaviour {
    public Transform BulletSpawn;

    [System.Serializable]
    public class AttackEvent : UnityEvent<Weapon> { }

    public AttackEvent OnAttack, OnMisfire;

    private Equipper equipper;
    private Aimer aimer;

    private Coroutine shootingProcess = null;

    [SerializeField]
    [ReadOnly]
    private bool isShooting = false;

    private GameInputManager inputManager;

    private void Start () {
        equipper = GetComponent<Equipper> ();
        aimer = GetComponent<Aimer> ();
        inputManager = GameContainer.InputManager ();
    }

    void Update () {
        if (inputManager.GetKey (ActionCode.Attack)) {
            if (!isShooting) {
                shootingProcess = startShooting ();
            }
        } else if (isShooting && shootingProcess != null) {
            stopShooting (shootingProcess);
        }
    }

    private Coroutine startShooting () {
        Weapon weapon = equipper.GetActiveWeapon ();
        Ammo ammo = weapon.CurrentAmmoType;
        if (ammo) {
            isShooting = true;
            return StartCoroutine (weapon.GetAttackType ().Fire (weapon, ammo, delegate {

                int ammoCountForShot = weapon.Buckshot ? 1 : weapon.ProjectileCountPerShot;
                if (ammoCountForShot > weapon.CurrentAmmoCount) {
                    OnMisfire.Invoke (weapon);
                    playSound (weapon.MisfireSound);
                    return;
                }
                GameObject prefab = ammo.Projectile;
                for (int i = 0; i < weapon.ProjectileCountPerShot; i++) {
                    GameObject projectile = GameContainer.ObjectPooler ().Spawn (prefab, BulletSpawn.position, BulletSpawn.rotation);
                    Disintegrating disintegrating = projectile.GetComponent<Disintegrating> ();
                    if (disintegrating) {
                        disintegrating.SetDisintegratingDistance (weapon.ProjectileRange);
                    }
                    projectile.GetComponent<Rigidbody2D> ().velocity = aimer.GetAimVector () * weapon.ProjectileInitVelocity;
                }
                weapon.CurrentAmmoCount -= ammoCountForShot;
                playSound (weapon.AttackSound);
                OnAttack.Invoke (weapon);
            }));
        }
        return null;
    }

    private void stopShooting (Coroutine shootingProcess) {
        isShooting = false;
        StopCoroutine (shootingProcess);
    }

    private void playSound (AudioClip clip) {
        if (!clip) {
            return;
        }
        AudioSource.PlayClipAtPoint (clip, transform.position);
    }
}
