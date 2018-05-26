using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent (typeof (Diable))]
public class Damageable : MonoBehaviour {

    [SerializeField]

    private int hp = 100;

    [SerializeField]
    private int maximumHp = 100;

    private Diable diable;

    [TestButton ("Take 10 Damage", "testTakeDamage", isActiveInEditor = false)]
    public UnityEvent OnTakeDamage;

    private void testTakeDamage () {
        TakeDamage (10);
    }

    private void Start () {
        diable = GetComponent<Diable> ();
    }

    public void TakeDamage (int damage) {
        hp -= (hp - damage <= 0) ? hp : damage;
        OnTakeDamage.Invoke ();
        if (hp == 0) {
            diable.Die ();
        }
    }

    public int GetCurrentHp () {
        return hp;
    }

    public int GetMaximumHp () {
        return maximumHp;
    }
}
