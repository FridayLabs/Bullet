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
    public float MaximumSpread = 10f;

    [Space (3)]

    [Range (0, 50)]
    [BoxGroup ("Aiming")]
    public float SpreadPerShoot = 1f;

    [Range (0, 50)]
    [BoxGroup ("Aiming")]
    public float AimSpreadPerShoot = 1f;

    [Space (3)]

    [Range (0, 50)]
    [BoxGroup ("Aiming")]
    public float SpreadOnMovePerShoot = 1f;

    [Range (0, 50)]
    [BoxGroup ("Aiming")]
    public float SpreadOnSeatPerShoot = 1f;

    [Space (3)]

    [Range (0, 5)]
    [BoxGroup ("Aiming")]
    public float SpreadRestoreSpeed = 1f;

    [Range (0, 5)]
    [BoxGroup ("Aiming")]
    public float SpreadRestoreDelay = 1f;

    [BoxGroup ("Atacking")]

    [Range (0, 5000)]
    public float ReloadTimeMs = 25f;
    [BoxGroup ("Atacking")]
    public float AttackCooldownMs = 500f;

    [Space (3)]
    [Range (10, 100)]
    [BoxGroup ("Atacking")]
    public float ProjectileInitVelocity = 25f;

    [Range (3, 50)]
    [BoxGroup ("Atacking")]
    public float ProjectileRange = 6f;

    [BoxGroup ("Atacking")]
    public int ProjectileCountPerShoot = 1;

    [Range (1, 100)]
    [BoxGroup ("Atacking")]
    public int DamagePerProjectile = 20;

    [BoxGroup ("Atacking")]
    public int MagazineCount = 7;

    [BoxGroup ("Atacking")]
    public float MeleeAttackRadius = 1f;
}
