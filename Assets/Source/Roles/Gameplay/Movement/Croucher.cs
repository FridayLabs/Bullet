using NaughtyAttributes;
using UnityEngine;

public class Croucher : MonoBehaviour {

    [SerializeField]
    [ReadOnly]
    private bool isCrouching = false;

    public bool IsCrouching () {
        return isCrouching;
    }

    private void Update () {
        if (GameContainer.InputManager ().GetKey (ActionCode.Crouch)) {
            if (!isCrouching) {
                crouch ();
            }
        } else if (isCrouching) {
            standup ();
        }
    }

    private void crouch () {
        isCrouching = true;
    }
    private void standup () {
        isCrouching = false;
    }
}
