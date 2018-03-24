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

    void Start () {
        equipper = GetComponent<Equipper> ();
        aimer = GetComponent<Aimer> ();
    }

    void Update () {
        if (Input.GetButtonDown ("Fire1")) {
            Weapon weapon = equipper.GetActiveWeapon ();

            Ammo ammo = equipper.GetActiveAmmo ();
            if (ammo) {
                GameObject prefab = ammo.Projectile;
                for (int i = 0; i < weapon.ProjectileCountPerShoot; i++) {
                    GameObject projectile = GameContainer.ObjectPooler ().Spawn (prefab, BulletSpawn.position, BulletSpawn.rotation);
                    projectile.GetComponent<Rigidbody2D> ().velocity = aimer.GetAimVector () * weapon.ProjectileInitVelocity;
                }

                OnAttack.Invoke (weapon);

            }
        }
    }
}
