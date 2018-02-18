using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HittableBulletController : MonoBehaviour {
    public UnityEvent OnBulletStop = new UnityEvent();
    public GameObject GrabbableBulletPrefab;
    
    private GameObject _bulletEnd;

    public void SetBulletEnd(GameObject bulletEnd) {
        _bulletEnd = bulletEnd;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log(_bulletEnd);
        Debug.Log(other.gameObject);
        if (other.gameObject == _bulletEnd) {
            Debug.Log("Bullet Stop");
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