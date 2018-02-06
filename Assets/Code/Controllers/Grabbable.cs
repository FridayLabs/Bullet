using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Grabbable : MonoBehaviour {

	private void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.tag == "Player") {
			var asd = other.gameObject.GetComponent<PlayerShootController>();
			if (asd.canEquip) {
				asd.GrabbedBullet.Invoke();
			
				gameObject.SetActive(false);				
			}
			
		}
	}
}
