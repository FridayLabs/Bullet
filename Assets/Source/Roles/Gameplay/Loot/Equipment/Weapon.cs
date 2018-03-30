using System;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class Weapon : Equipment {
    [BoxGroup ("UI")]
    public Texture2D Crosshair;

    [BoxGroup ("Aiming")]
    [Range (0, 50)]
    public float DefaultOverviewRange = 6f;
    [BoxGroup ("Aiming")]
    [Range (0, 50)]
    public float AimOverviewRange = 10f;
    [Space (3)]

    [Range (0, 100)]
    [BoxGroup ("Aiming")]
    public float DefaultSpread = 0f;

    [Range (0, 100)]
    [BoxGroup ("Aiming")]
    public float MaximumSpread = 10f;

    [Space (3)]

    [Range (0, 50)]
    [BoxGroup ("Aiming")]
    public float SpreadPerShot = 1f;

    [Range (0, 50)]
    [BoxGroup ("Aiming")]
    public float AimSpreadPerShot = 1f;

    [Space (3)]

    [Range (0, 50)]
    [BoxGroup ("Aiming")]
    public float SpreadOnMovePerShot = 1f;

    [Range (0, 50)]
    [BoxGroup ("Aiming")]
    public float SpreadOnSeatPerShot = 1f;

    [Space (3)]

    [Range (0, 5)]
    [BoxGroup ("Aiming")]
    public float SpreadRestoreSpeed = 1f;

    [Range (0, 5)]
    [BoxGroup ("Aiming")]
    public float SpreadRestoreDelay = 1f;

    [BoxGroup ("Atacking")]
    public AudioClip AttackSound;

    [BoxGroup ("Atacking")]
    public AudioClip MisfireSound;

    [Space (3)]
    [Range (10, 100)]
    [BoxGroup ("Atacking")]
    public float ProjectileInitVelocity = 25f;

    [Range (3, 50)]
    [BoxGroup ("Atacking")]
    public float ProjectileRange = 6f;

    [BoxGroup ("Atacking")]
    public bool Buckshot = false;

    [BoxGroup ("Atacking")]
    [ShowIf ("Buckshot")]
    [MinValue (1)]
    public int ProjectileCountPerShot = 1;

    [BoxGroup ("Atacking")]
    [MinValue (1)]
    [ShowIf ("hasBurstedAttack")]
    public int ProjectileCountPerBurst = 1;

    [BoxGroup ("Atacking")]
    [Range (1, 100)]
    public int DamagePerProjectile = 20;

    [BoxGroup ("Atacking")]
    public int AttackRate = 600;

    [BoxGroup ("Atacking")]
    [MinValue (1)]
    public int MagazineCount = 7;

    [BoxGroup ("Atacking")]
    [ReadOnly]
    public Ammo CurrentAmmoType;

    [BoxGroup ("Atacking")]
    [ReadOnly]
    public int CurrentAmmoCount;

    [BoxGroup ("Atacking")]
    [Range (1, 100)]
    public float MeleeAttackRadius = 1f;

    [BoxGroup ("Atacking")]
    [SerializeField]
    [ReorderableList]
    private List<AttackType> AttackTypes;
    private int currentAttackTypeIdx = 0;

    [BoxGroup ("Reloading")]
    public AudioClip ReloadingStartSound;

    [BoxGroup ("Reloading")]
    public AudioClip ReloadingSound;

    [BoxGroup ("Reloading")]
    public AudioClip ReloadingFinishSound;

    [BoxGroup ("Reloading")]
    public ReloadType ReloadType;

    [BoxGroup ("Reloading")]
    [Range (0, 5000)]
    public float ReloadPerIteration = 250f;

    public AttackType GetAttackType () {
        if (currentAttackTypeIdx > AttackTypes.Count - 1) {
            throw new Exception (this.FriendlyName + " has no AttackType with idx " + currentAttackTypeIdx);
        }
        return AttackTypes[currentAttackTypeIdx];
    }

    private bool hasBurstedAttack () {
        return AttackTypes.FindIndex ((AttackType x) => x.BurstedFire == true) != -1;
    }
}
