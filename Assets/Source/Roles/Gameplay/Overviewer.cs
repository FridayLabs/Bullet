using UnityEngine;

public class Overviewer : MonoBehaviour {
    public Transform Target;

    public float LerpingSpeed = 25f;
    private float overviewRadius = 5f;

    public void SetOverviewAimRadius (Weapon weapon) {
        overviewRadius = weapon.AimOverviewRange;
    }

    public void SetOverviewDefaultRadius (Weapon weapon) {
        overviewRadius = weapon.DefaultOverviewRange;
    }

    void LateUpdate () {
        Vector2 cursorPos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
        Vector2 playerPos = Target.position;

        Vector2 diff = (cursorPos - playerPos) * (3f / 4f);
        diff = Vector2.ClampMagnitude (diff, overviewRadius);

        Vector2 cameraPos = playerPos + diff;

        transform.position = Vector3.Lerp (
            transform.position,
            new Vector3 (cameraPos.x, cameraPos.y, transform.position.z),
            Time.deltaTime * LerpingSpeed
        );
    }
}
