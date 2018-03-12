using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponOwner : MonoBehaviour {

    [SerializeField] private Weapon weapon;

    public Weapon GetWeapon () {
        return weapon;
    }
}
