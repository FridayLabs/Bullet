using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Dieable : MonoBehaviour {
	public bool IsAlive = true;
	public UnityEvent OnDie = new UnityEvent();
	public UnityEvent OnResurect = new UnityEvent();
	
	private StatsHolder _stats;

	void Start() {
		_stats = GetComponent<StatsHolder>();
	}

	void Update() {
		var oldIsAlive = IsAlive;
		IsAlive = _stats.GetStatValue("HP") > 0;

		if (oldIsAlive && !IsAlive) {
			OnDie.Invoke();
		}
		if (!oldIsAlive && IsAlive) {
			OnResurect.Invoke();
		}
	}
}
