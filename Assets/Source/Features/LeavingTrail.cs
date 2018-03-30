using UnityEngine;

public class LeavingTrail : MonoBehaviour {
    public void LeaveTrail () {
        RaycastHit2D hit = Physics2D.Raycast (transform.position, Vector2.zero);
        if (hit && hit.collider) {
            ConsistingOfSubstance consistingOfSubstance = hit.collider.GetComponent<ConsistingOfSubstance> ();
            if (consistingOfSubstance) {
                Substance substance = consistingOfSubstance.GetSubstance ();
                // Debug.Log ("Playing effect of hitting " + substance);
                // Debug.Log ("Leaving trail on " + hit.collider.name);
            }
        }
    }
}
