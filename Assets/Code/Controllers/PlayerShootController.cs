using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerShootController : MonoBehaviour {
	public Perk Perk;

	public bool canShoot;
	public bool canEquip = true;
	
	public GameObject _bullet;

	public UnityEvent GrabbedBullet = new UnityEvent();
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButton("Fire1")) {
			_bullet = GameObject.FindGameObjectWithTag("Bullet");
			_bullet.SetActive(true);
//			_bullet.transform.position = transform.position + Vector3.forward * 200;
//			_bullet.transform.rotation = transform.rotation;
			_bullet.GetComponent<Rigidbody2D>().AddForce(transform.forward.normalized * Perk.weapon.BulletStartVelosity * 50);
			canShoot = false;
		}
	}

	public void EquipBullet() {
		if (canEquip) {
			canEquip = false;
		}
		canShoot = true;
		_bullet = GameObject.FindGameObjectWithTag("Bullet");
	}
}
