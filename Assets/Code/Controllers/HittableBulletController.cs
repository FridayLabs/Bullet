using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public class HittableBulletController : NetworkBehaviour {
    public GameObject GrabbableBulletPrefab;
    
    private GameObject _bulletEnd;

    public void SetBulletEnd(GameObject bulletEnd) {
        _bulletEnd = bulletEnd;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject == _bulletEnd) {
            HandleBulletCollision();
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.GetComponent<BulletStoppable>()) {
            HandleBulletCollision();
        }
    }

    private void HandleBulletCollision() {
        CmdDestroy();
        CmdSpawnGrabbableBullet();
    }
    
    [Command]
    public void CmdSpawnGrabbableBullet() {
        var bullet = Instantiate(GrabbableBulletPrefab, transform.position, transform.rotation);
        NetworkServer.Spawn(bullet);
    }

    [Command]
    public void CmdDestroy() {
        Destroy(gameObject);
    }
}