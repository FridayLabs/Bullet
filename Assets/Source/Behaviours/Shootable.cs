using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(WeaponOwner))]
[RequireComponent(typeof(Aimable))]
public class Shootable : MonoBehaviour {
    public Transform BulletSpawn;

    public UnityEvent OnShoot;
    public UnityEvent OnRealoadMisfire;

    private WeaponOwner weaponOwner;
    private Aimable aim;

    private float lastShootTime;

    private int currentBulletsInMagazine;

    void Start() {
        aim = GetComponent<Aimable>();
        weaponOwner = GetComponent<WeaponOwner>();

        lastShootTime = Time.fixedTime;
        currentBulletsInMagazine = weaponOwner.GetWeapon().MagazineCount;
    }

    void FixedUpdate() {
        if (Input.GetButton("Fire1") && IsShootCooldownExpired()) {
            if (currentBulletsInMagazine <= 0) {
                OnRealoadMisfire.Invoke();
                return;
            }

            shoot();
            OnShoot.Invoke();
            lastShootTime = Time.fixedTime;
            currentBulletsInMagazine--;
        }
    }

    private bool IsShootCooldownExpired() {
        return Time.fixedTime - lastShootTime > weaponOwner.GetWeapon().ShootCooldown;
    }

    private void shoot() {
        var dir = aim.GetAimVector();
        var prefab = weaponOwner.GetWeapon().ProjectilePrefab;
        var bullet = Instantiate(prefab, BulletSpawn.position, BulletSpawn.rotation);
        bullet.GetComponent<Rigidbody2D>().velocity = dir * weaponOwner.GetWeapon().BulletVelocity;
    }
}