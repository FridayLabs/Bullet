using UnityEngine;

public class CameraController : MonoBehaviour {
    public Transform Player;

    private float overviewRadius = 5f;

    public void SetOverviewRadius(float rad) {
        overviewRadius = rad;
    }

    void LateUpdate() {
        Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 playerPos = Player.position;

        Vector2 diff = (cursorPos - playerPos) / 2f;
        diff = Vector2.ClampMagnitude(diff, overviewRadius);

        Vector2 cameraPos = playerPos + diff;

        transform.position = Vector3.Lerp(transform.position,
            new Vector3(cameraPos.x, cameraPos.y, transform.position.z),
            Time.deltaTime * 10f);
    }
}