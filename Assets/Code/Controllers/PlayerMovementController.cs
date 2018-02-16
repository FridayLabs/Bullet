using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovementController : MonoBehaviour {
    public Perk Perk;

    private Rigidbody2D _rigidbody2D;
    private const int movementModificator = 20;

    void Start() {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate() {
        Vector2 movementVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        _rigidbody2D.velocity = movementVector.normalized * Perk.MovementSpeed * movementModificator * Time.deltaTime;

        var pos = Camera.main.WorldToScreenPoint(transform.position);
        var dir = Input.mousePosition - pos;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}