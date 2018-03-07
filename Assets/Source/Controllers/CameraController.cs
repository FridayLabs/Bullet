using UnityEngine;

public class CameraController : MonoBehaviour {

	public Transform Player;

	void LateUpdate () {
		var cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		var playerPos = Player.position;
		var cameraPos = (cursorPos + playerPos) / 2.0f;
		transform.position = cameraPos; //TODO smooth
	}
}
