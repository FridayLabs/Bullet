using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovementController : MonoBehaviour {
    private Rigidbody2D _rigidbody2D;
    private StatsHolder _stats;
    private const int movementModificator = 20;
    private Animator _animator;

    void Start() {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _stats = GetComponent<StatsHolder>();
        _animator = GetComponent<Animator>();
    }

    void FixedUpdate() {
        // movement
        Vector2 movementVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        _rigidbody2D.velocity = movementVector.normalized * _stats.GetStatValue("Movespeed") * movementModificator * Time.deltaTime;

        // setting walk animation
		_animator.SetBool("Walk", _rigidbody2D.velocity != Vector2.zero);

        // rotation
        var pos = Camera.main.WorldToScreenPoint(transform.position);
        var dir = Input.mousePosition - pos;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}