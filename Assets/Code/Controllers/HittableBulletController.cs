using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HittableBulletController : MonoBehaviour {
    public UnityEvent OnBulletStop = new UnityEvent();
    public UnityEvent OnBulletWallCollide = new UnityEvent();
    public GameObject GrabbableBulletPrefab;
    
    private GameObject _bulletEnd;

    public void SetBulletEnd(GameObject bulletEnd) {
        _bulletEnd = bulletEnd;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject == _bulletEnd) {
            OnBulletStop.Invoke();
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.GetComponent<BulletStoppable>()) {
            OnBulletWallCollide.Invoke();
            OnBulletStop.Invoke();
        }
    }

    public void SpawnGrabbableBullet() {
        Instantiate(GrabbableBulletPrefab, transform.position, transform.rotation);
    }

    public void Destroy() {
        Destroy(gameObject);
    }
}