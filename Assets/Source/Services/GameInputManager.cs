using UnityEngine;

public class GameInputManager {

    public bool GetKeyDown (ActionCode action) {
        return Input.GetKeyDown (action.KeyCode);
    }

    public bool GetKey (ActionCode action) {
        return Input.GetKey (action.KeyCode);
    }

    public ActionCode GetSomeKeyDown (ActionCode[] actions) {
        foreach (ActionCode action in actions) {
            if (GetKeyDown (action)) {
                return action;
            }
        }
        return null;
    }

    public ActionCode GetSomeKey (ActionCode[] actions) {
        foreach (ActionCode action in actions) {
            if (GetKey (action)) {
                return action;
            }
        }
        return null;
    }
}
