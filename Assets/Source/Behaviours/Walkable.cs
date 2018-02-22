using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Walkable : MonoBehaviour {

	//TODO define player input source

	public Rigidbody2D Rigidbody2D;
	
	[Range(1, 20)]
	public float MoveSpeed = 5f;

	public Animator WalkAnimator;
	
	public UnityEvent OnWalkStart;
	public UnityEvent OnWalkStop;

	private bool isWalking = false;
	private float movementModifier = 20f;
	
	void FixedUpdate () {
		Vector2 movementVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")); // TODO
		Rigidbody2D.velocity = movementVector.normalized *MoveSpeed * movementModifier * Time.deltaTime;

		if (!isWalking && Rigidbody2D.velocity != Vector2.zero) {
			WalkAnimator.SetBool("IsWalking", true);
			OnWalkStart.Invoke();
			isWalking = true;
		}
		
		if (isWalking && Rigidbody2D.velocity == Vector2.zero) {
			WalkAnimator.SetBool("IsWalking", false);
			OnWalkStop.Invoke();
			isWalking = false;
		}
		
		// rotation
		var pos = Camera.main.WorldToScreenPoint(transform.position);
		var dir = Input.mousePosition - pos;
		var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
	}
}
