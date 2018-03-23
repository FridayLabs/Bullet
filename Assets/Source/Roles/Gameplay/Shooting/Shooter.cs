using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent (typeof (Equipper))]
[RequireComponent (typeof (Aimer))]
public class Shooter : MonoBehaviour {
    public Transform BulletSpawn;
    public GameObject prefab;

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

            var bullet = (GameObject) Instantiate (prefab, BulletSpawn.position, BulletSpawn.rotation);

            bullet.GetComponent<Rigidbody2D> ().velocity = aimer.GetAimVector () * weapon.ProjectileInitVelocity;
            Destroy (bullet, 1f);

            OnAttack.Invoke (weapon);

        }
    }
}
