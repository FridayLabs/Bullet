using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public Transform Player;
	public bool FollowPlayer = true;

	public float AimCameraMovespeed = 5f;
	public float AimCameraEdgeWidth = 10f;

	public void FollowPlayerMode(bool followPlayer) {
		FollowPlayer = followPlayer;
	}
	
	// Update is called once per frame
	void Update () {
		if (FollowPlayer) {
			followPlayerUpdate();
		} else {
			mapOverviewUpdate();
		}
	}
	
	private void followPlayerUpdate () {
		var currentPosition = transform.position;
		currentPosition.x = Player.transform.position.x;
		currentPosition.y = Player.transform.position.y;
		
		transform.position = currentPosition;
	}

	private void mapOverviewUpdate() {
		var mousePosition = Input.mousePosition;

		var movementVector = new Vector3();

		if (mousePosition.x <= AimCameraEdgeWidth) {
			movementVector.x = -AimCameraMovespeed;
		}
		if (mousePosition.x >= Screen.width - AimCameraEdgeWidth) {
			movementVector.x = AimCameraMovespeed;
		}

		if (mousePosition.y <= AimCameraEdgeWidth) {
			movementVector.y = -AimCameraMovespeed;
		}
		
		if (mousePosition.y >= Screen.height - AimCameraEdgeWidth) {
			movementVector.y = AimCameraMovespeed;
		}

		transform.localPosition += movementVector * Time.fixedDeltaTime;
	}
}
