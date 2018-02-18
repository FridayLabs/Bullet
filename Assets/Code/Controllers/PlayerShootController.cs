using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerShootController : MonoBehaviour {
    public UnityEvent OnShoot = new UnityEvent();
    
    public GameObject BulletProjectile;
    public Transform BulletSpawn;
    public GameObject BulletEnd;

    private WeaponHolder _weaponHolder;
    private BulletHolder _bulletHolder;

    // Use this for initialization
    void Start() {
        _weaponHolder = GetComponent<WeaponHolder>();
        _bulletHolder = GetComponent<BulletHolder>();

        BulletEnd.transform.localPosition += new Vector3(_weaponHolder.weapon.BulletRange, 0);
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (Input.GetButton("Fire1") && _bulletHolder.HasBullet) {
            Shoot();
        }
    }

    private void Shoot() {
        var bullet = Instantiate(BulletProjectile, BulletSpawn.position, BulletSpawn.rotation);
        var shootVector = (GetChild("Aim").transform.position - BulletSpawn.position).normalized;
        
        bullet.GetComponent<Rigidbody2D>().velocity = shootVector * _weaponHolder.weapon.BulletStartVelosity;
        bullet.GetComponent<HittableBulletController>().SetBulletEnd(BulletEnd);
        _bulletHolder.HasBullet = false;
        
        OnShoot.Invoke();
    }
    
    
    private Transform GetChild(string name) {
        foreach (Transform child in GetComponentsInChildren<Transform>(true)) {
            if (child.gameObject.name == name) {
                return child;
            }
        }
        throw new Exception("Player has not " + name);
    }
}