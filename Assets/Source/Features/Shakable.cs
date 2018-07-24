using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shakable : MonoBehaviour {

    public void Shake (float force) {
        Transform target = gameObject.transform;

        shakeTarget (target, force, 1);
    }

    private void shakeTarget (Transform target, float force, int impulseCount) {
        Vector3 initPoint = target.position;
        for (int i = 0; i < impulseCount; i++) {
            Vector3 newPoint = new Vector3 (
                initPoint.x * Mathf.PerlinNoise (initPoint.x, initPoint.y) * force,
                initPoint.y * Mathf.PerlinNoise (initPoint.x, initPoint.y) * force,
                initPoint.z
            );

            for (int j = 0; j < 3; j++) {
                target.position = new Vector3 (
                    Mathf.Lerp (initPoint.x, newPoint.x, Time.deltaTime),
                    Mathf.Lerp (initPoint.y, newPoint.y, Time.deltaTime),
                    initPoint.z
                );
            }
            for (int j = 0; j < 3; j++) {
                target.position = new Vector3 (
                    Mathf.Lerp (target.position.x, initPoint.x, Time.deltaTime),
                    Mathf.Lerp (target.position.y, initPoint.y, Time.deltaTime),
                    initPoint.z
                );
            }

        }
    }
}
