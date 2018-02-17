using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAimController : MonoBehaviour {
    private Rigidbody2D _rigidBody;
    private WeaponHolder _weaponHolder;
    private int _aimMovingSide = -1;
    private float _currentAimArcLength;
    private Vector3 _initialAimTransform;

    // Use this for initialization
    void Start() {
        _rigidBody = GetComponent<Rigidbody2D>();
        _weaponHolder = GetComponent<WeaponHolder>();
        _currentAimArcLength = _weaponHolder.weapon.AimArcLength;
        _initialAimTransform = GetChild("Aim").localPosition;
        Debug.Log(_initialAimTransform);
    }

    // Update is called once per frame
    void FixedUpdate() {
        TriggerAim(Input.GetButton("Fire2"));
        UpdateAimArcLength();
        UpdateAimPosition();
        UpdateArcScale();
    }

    void TriggerAim(bool showAim) {
        GetChild("AimContainer").gameObject.SetActive(showAim);
    }

    void UpdateAimArcLength() {
        var newAimArcLength = _rigidBody.velocity == Vector2.zero
            ? _weaponHolder.weapon.AimArcLength
            : _weaponHolder.weapon.AimArcLength * _weaponHolder.weapon.MovementModifier;
        if (newAimArcLength != _currentAimArcLength) {
            _currentAimArcLength = newAimArcLength;
            ResetAimPosition();
        }
    }

    void UpdateAimPosition() {
        var aim = GetChild("Aim");
        var aimArc = GetChild("AimArc");
        Vector3 dir = aim.position - transform.position;

        Vector2 forwardVec = transform.position - aimArc.position;
        Vector2 aimVec = transform.position - aim.position;

        var angle = Vector2.Angle(forwardVec, aimVec);

        if (Mathf.DeltaAngle(angle, _currentAimArcLength) * 10 <= 0.01) {
            _aimMovingSide *= -1;
        }

        dir = Quaternion.Euler(_aimMovingSide * Vector3.forward * _weaponHolder.weapon.AimSpeed) * dir;
        aim.position = dir + transform.position;
    }

    void UpdateArcScale() {
        var aimArc = GetChild("AimArc");
        aimArc.localScale = new Vector3(0.1f, Mathf.PI * 10f * _currentAimArcLength / 180, 0);
    }

    void ResetAimPosition() {
        GetChild("Aim").localPosition = _initialAimTransform;
    }
    
    private Transform GetChild(string name) {
        foreach (Transform child in GetComponentsInChildren<Transform>(true)) {
            if (child.gameObject.name == name) {
                return child;
            }
        }
        throw new Exception("Player has not " + name);
    }
}