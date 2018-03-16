using UnityEngine;

[RequireComponent (typeof (WeaponOwner))]
public class Aimer : MonoBehaviour {
    public Vector2 GetAimVector () {
        return transform.forward.normalized;
    }
}
