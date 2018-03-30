using UnityEngine;
using UnityEngine.Events;

[RequireComponent (typeof (Rigidbody2D))]
public class Walker : MonoBehaviour {
    [Range (1, 20)]
    public float MoveSpeed = 10f;

    public Animator WalkAnimator;

    public UnityEvent OnWalkStart;
    public UnityEvent OnWalkStop;

    new private Rigidbody2D rigidbody;

    private bool isWalking = false;
    private float movementModifier = 20f;
    private GameInputManager input;

    public bool IsWalking () {
        return isWalking;
    }

    private void Awake () {
        rigidbody = GetComponent<Rigidbody2D> ();
        input = GameContainer.InputManager ();
    }

    void FixedUpdate () {

        float x = 0;
        if (input.GetKey (ActionCode.MoveLeft)) {
            x = -1;
        } else if (input.GetKey (ActionCode.MoveRight)) {
            x = 1;
        }

        float y = 0;
        if (input.GetKey (ActionCode.MoveUp)) {
            y = 1;
        } else if (input.GetKey (ActionCode.MoveDown)) {
            y = -1;
        }
        rigidbody.velocity = new Vector2 (x, y) * MoveSpeed * movementModifier * Time.fixedDeltaTime;

        if (!isWalking && rigidbody.velocity != Vector2.zero) {
            WalkAnimator.SetBool ("IsWalking", true);
            OnWalkStart.Invoke ();
            isWalking = true;
        }

        if (isWalking && rigidbody.velocity == Vector2.zero) {
            WalkAnimator.SetBool ("IsWalking", false);
            OnWalkStop.Invoke ();
            isWalking = false;
        }

        // rotation
        var pos = Camera.main.WorldToScreenPoint (transform.position);
        var dir = Input.mousePosition - pos;
        var angle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis (angle, Vector3.forward);
    }
}
