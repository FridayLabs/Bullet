using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHolder : MonoBehaviour {
	public bool HasBullet = false;

	public void GrabBullet() {
		HasBullet = true;
	}
}
