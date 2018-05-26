using UnityEngine;
using UnityEngine.Events;

public class Diable : MonoBehaviour {

    public UnityEvent OnDie;

    public void Die () {
        OnDie.Invoke ();
    }
}
