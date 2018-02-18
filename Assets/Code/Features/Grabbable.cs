using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Grabbable : MonoBehaviour {
	private void OnCollisionEnter2D(Collision2D other) {
		var holder = other.gameObject.GetComponent<BulletHolder>();
		if (holder) {
			holder.CmdGrabBullet();
			Destroy(gameObject);
		}
	}
}
