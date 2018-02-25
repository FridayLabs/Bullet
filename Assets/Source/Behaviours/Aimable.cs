using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(WeaponOwner))]
public class Aimable : MonoBehaviour {
    public GameObject AimContainer;
    public GameObject Aim;
    public GameObject AimArc;

    public bool HideAimByDefault = true;

    public UnityEvent OnPlayerAimStart;
    public UnityEvent OnPlayerAimStop;

    private int aimMovingSide = -1;
    private float currentAimArcLength;
    private Vector3 initialAimTransform;

    private WeaponOwner weaponOwner;

    void Start() {
        weaponOwner = GetComponent<WeaponOwner>();

        // Remember initiali Aim position for resetting 
        initialAimTransform = Aim.transform.localPosition;

        // Setting AimContainer to proper range from player
        AimContainer.transform.localPosition = new Vector2(weaponOwner.GetWeapon().AimRange, 0);

        // Set Aim Sprite
        Aim.GetComponent<SpriteRenderer>().sprite = weaponOwner.GetWeapon().AimSprite;

        SetAimWhileStanding();
    }

    void FixedUpdate() {
        // TODO Get input from system somehow..
        TriggerAim(!HideAimByDefault || Input.GetButton("Fire2"));
        UpdateAimPosition();
    }

    void TriggerAim(bool showAim) {
        if (!AimContainer.activeSelf && showAim) {
            AimContainer.SetActive(true);
            OnPlayerAimStart.Invoke();
        }

        if (AimContainer.activeSelf && !showAim) {
            AimContainer.SetActive(false);
            OnPlayerAimStop.Invoke();
        }
    }

    public void SetAimWhileMoving() {
        SetAimArcLength(weaponOwner.GetWeapon().SpreadLengthWhileMovement);
    }

    public void SetAimWhileStanding() {
        SetAimArcLength(weaponOwner.GetWeapon().SpreadLength);
    }

    private void SetAimArcLength(float newAimArcLength) {
        if (newAimArcLength != currentAimArcLength) {
            currentAimArcLength = newAimArcLength;
            UpdateArcScale(currentAimArcLength);
            ResetAimPosition();
        }
    }

    void UpdateAimPosition() {
        Vector3 dir = Aim.transform.position - transform.position;

        Vector2 forwardVec = transform.position - AimArc.transform.position;
        Vector2 aimVec = transform.position - Aim.transform.position;

        var angle = Vector2.Angle(forwardVec, aimVec) * 2;

        if (Mathf.DeltaAngle(angle, currentAimArcLength) * 10 <= 0.01) {
            aimMovingSide *= -1;
        }

        dir = Quaternion.Euler(aimMovingSide * Vector3.forward * weaponOwner.GetWeapon().AimSpeed) * dir;
        Aim.transform.position = dir + transform.position;
    }

    void UpdateArcScale(float angle) {
        var line = AimArc.gameObject.GetComponent<LineRenderer>();

        var resolution = 10;
        line.SetVertexCount(resolution + 1);

        var vec = AimArc.transform.position - transform.position;
        var radius = vec.magnitude;

        var a = -angle / 2;
        var d = angle / resolution;
        for (var i = 0; i <= resolution; i++) {
            var x = Mathf.Cos(Mathf.Deg2Rad * a) * radius - radius;
            var y = Mathf.Sin(Mathf.Deg2Rad * a) * radius;

            line.SetPosition(i, new Vector3(x, y, 0));

            a += d;
        }
    }

    void ResetAimPosition() {
        Aim.transform.localPosition = initialAimTransform;
    }
}