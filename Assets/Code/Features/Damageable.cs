using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour {
	public UnityEvent OnTakeDamage = new UnityEvent();
	
	private StatsHolder _stats;

	void Start() {
		_stats = GetComponent<StatsHolder>();
	}
	
	public void TakeDamage(float damage) {
		var hp = _stats.GetStatValue("HP");
		_stats.SetStatValue("HP", hp - damage);
		
		OnTakeDamage.Invoke();
	}
}
