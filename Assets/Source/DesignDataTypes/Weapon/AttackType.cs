using System;
using System.Collections;
using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu (fileName = "AttackType", menuName = "Weapon/AttackType")]
public class AttackType : DataType {

    public bool BurstedFire = true;

    [ShowIf ("BurstedFire")]
    public float DelayBetweenBurst = 1f;

    public IEnumerator Fire (Weapon weapon, Ammo ammo, Action attack) {
        float attackCd = 60f / (float) weapon.AttackRate;

        while (true) {
            if (!BurstedFire) {
                attack ();
                yield return new WaitForSeconds (attackCd);
            } else {
                for (int i = 0; i < weapon.ProjectileCountPerBurst; i++) {
                    attack ();
                    yield return new WaitForSeconds (attackCd);
                }
                yield return new WaitForSeconds (DelayBetweenBurst);
            }
        }
    }
}
