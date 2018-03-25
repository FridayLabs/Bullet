using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent (typeof (Equipper))]
[RequireComponent (typeof (Aimer))]
public class Shooter : MonoBehaviour {
    public Transform BulletSpawn;

    [System.Serializable]
    public class AttackEvent : UnityEvent<Weapon> { }

    public AttackEvent OnAttack;

    private Equipper equipper;
    private Aimer aimer;

    private Coroutine shootingProcess = null;
    private bool isShooting = false;

    void Start () {
        equipper = GetComponent<Equipper> ();
        aimer = GetComponent<Aimer> ();
    }

    void Update () {
        if (Input.GetButton ("Fire1")) {
            if (!isShooting) {
                shootingProcess = startShooting ();
            }
        } else if (isShooting) {
            stopShooting (shootingProcess);
        }
    }

    private Coroutine startShooting () {
        Weapon weapon = equipper.GetActiveWeapon ();
        Ammo ammo = equipper.GetActiveAmmo ();
        if (ammo) {
            isShooting = true;
            return StartCoroutine (weapon.GetAttackType ().Fire (weapon, ammo, delegate {
                GameObject prefab = ammo.Projectile;
                for (int i = 0; i < weapon.ProjectileCountPerShot; i++) {
                    GameObject projectile = GameContainer.ObjectPooler ().Spawn (prefab, BulletSpawn.position, BulletSpawn.rotation);
                    Disintegrating disintegrating = projectile.GetComponent<Disintegrating> ();
                    if (disintegrating) {
                        disintegrating.SetDisintegratingDistance (weapon.ProjectileRange);
                    }
                    projectile.GetComponent<Rigidbody2D> ().velocity = aimer.GetAimVector () * weapon.ProjectileInitVelocity;
                }
                OnAttack.Invoke (weapon);
            }));
        }
        return null;
    }

    private void stopShooting (Coroutine shootingProcess) {
        isShooting = false;
        StopCoroutine (shootingProcess);
    }
}
