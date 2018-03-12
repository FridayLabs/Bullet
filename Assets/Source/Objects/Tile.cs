using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {
	private void OnDrawGizmos() {

	}

	private void drawGizmo(Vector3 pos) {
		Gizmos.DrawCube(pos, Vector3.one / 3);
	}

	private void OnDrawGizmosSelected() {
//		Gizmos.color = UnityEngine.Color.white;
//		drawGizmo(new Vector3(transform.position.x - transform.localScale.x, transform.position.y, 0));
//		drawGizmo(new Vector3(transform.position.x + transform.localScale.x, transform.position.y, 0));
//		drawGizmo(new Vector3(0, transform.position.y - transform.localScale.y, 0));
//		drawGizmo(new Vector3(0, transform.position.y + transform.localScale.y, 0));
	}
}
