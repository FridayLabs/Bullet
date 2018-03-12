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

    private void Awake () {
        rigidbody = GetComponent<Rigidbody2D> ();
    }

    void FixedUpdate () {
        Vector2 movementVector = new Vector2 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical")); // TODO
        rigidbody.velocity = movementVector.normalized * MoveSpeed * movementModifier * Time.fixedDeltaTime;

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
