using UnityEngine;

[RequireComponent (typeof (Overviewer))]
[RequireComponent (typeof (Equipper))]
public class Aimer : MonoBehaviour {

    enum AimMode {
        Default,
        Aim
    }
    private AimMode aimMode;
    private Overviewer overviewer;
    private Equipper equipper;

    private void Start () {
        overviewer = GetComponent<Overviewer> ();
        equipper = GetComponent<Equipper> ();
    }

    void FixedUpdate () {
        if (Input.GetMouseButton (1)) { // TODO
            changeAimMode (AimMode.Aim);
        } else {
            changeAimMode (AimMode.Default);
        }
    }

    public Vector2 GetAimVector () {
        return transform.forward.normalized;
    }

    private void changeAimMode (AimMode mode) {
        // if (aimMode == mode || equipper.GetWeapon () == null) {
        //     return;
        // }
        // aimMode = mode;
        // overviewer.SetOverviewRadius (equipper.GetWeapon ().AimRange);
    }
}
