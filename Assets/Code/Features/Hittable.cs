﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public class Hittable : NetworkBehaviour {
	public float HitDamage = 1f;
	
	private void OnCollisionEnter2D(Collision2D other) {
		var damageable = other.gameObject.GetComponent<Damageable>();
		if (damageable) {
			damageable.CmdTakeDamage(HitDamage);
		}
	}
}