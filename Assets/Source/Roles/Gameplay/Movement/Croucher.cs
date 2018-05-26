using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

public class Croucher : MonoBehaviour {

    [SerializeField]
    [ReadOnly]
    private bool isCrouching = false;

    public UnityEvent OnCrouch, OnStandup;

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
        OnCrouch.Invoke ();
    }
    private void standup () {
        isCrouching = false;
        OnStandup.Invoke ();
    }
}
