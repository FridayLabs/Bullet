using UnityEngine;

public class ConsistingOfSubstance : MonoBehaviour {
    [SerializeField]
    private Substance substance;

    public Substance GetSubstance () {
        return substance;
    }
}
