using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAimController : MonoBehaviour {
    private WeaponHolder _weaponHolder;
    private float _aimRadius;

    private int _aimMovingSide = -1;

    // Use this for initialization
    void Start() {
        _weaponHolder = GetComponent<WeaponHolder>();
        _aimRadius = transform.position.x;
    }

    // Update is called once per frame
    void Update() {
//        TriggerAim(Input.GetButton("Fire2"));

        UpdateAimPosition();
    }

    void TriggerAim(bool showAim) {
        GetAim().gameObject.SetActive(showAim);
    }

    void UpdateAimPosition() {
        var aim = GetAim();
        var aimArc = GetAimArc();
        Vector3 dir = aim.position - transform.position;
        
        Vector2 forwardVec = transform.position - aimArc.position;
        Vector2 aimVec = transform.position - aim.position;
        
        var angle = Vector2.Angle(forwardVec, aimVec);

        if (_weaponHolder.weapon.AimArcLength - angle < 0) {
            _aimMovingSide *= -1;
        }
        
        dir = Quaternion.Euler(_aimMovingSide * Vector3.forward * _weaponHolder.weapon.AimSpeed) * dir;            
        
        aim.position = dir + transform.position;
    }

    private Transform GetAim() {
        foreach (Transform child in transform) {
            if (child.gameObject.tag == "Aim") {
                return child;
            }
        }
        throw new Exception("Player has not Aim");
    }
    private Transform GetAimArc() {
        foreach (Transform child in transform) {
            if (child.gameObject.tag == "AimArc") {
                return child;
            }
        }
        throw new Exception("Player has not AimArc");
    }
}