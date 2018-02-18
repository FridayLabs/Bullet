using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public class Damageable : NetworkBehaviour {
	public UnityEvent OnTakeDamage = new UnityEvent();
	
	private StatsHolder _stats;

	void Start() {
		_stats = GetComponent<StatsHolder>();
	}
	
	[Command]
	public void CmdTakeDamage(float damage) {
		var hp = _stats.GetStatValue("HP");
		_stats.SetStatValue("HP", hp - damage);
		
		OnTakeDamage.Invoke();
	}
}
