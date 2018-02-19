using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public class Dieable : NetworkBehaviour {

	public Sprite DeadSprite;
	
	[SyncVar]
	public bool IsAlive = true;

	private StatsHolder _stats;
	private Animator _animator;

	void Start() {
		_stats = GetComponent<StatsHolder>();
		_animator = GetComponent<Animator>();
	}

	void Update() {
		var oldIsAlive = IsAlive;
		IsAlive = _stats.GetStatValue("HP") > 0;

		if (oldIsAlive && !IsAlive) {
			RpcDie();
		}
	}

	[ClientRpc]
	void RpcDie() {
		GetComponent<PlayerMovementController>().enabled = false;
		GetComponent<PlayerAimController>().enabled = false;
		GetComponent<PlayerShootController>().enabled = false;
		GetComponent<SpriteRenderer>().sprite = DeadSprite;
		
		_animator.SetBool("IsAlive", false);
	}
}
