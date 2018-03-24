using UnityEngine;

public class Weapon : Equipment {
    public Texture2D Crosshair;

    [Header ("Aiming")]

    [Range (0, 50)]
    public float DefaultOverviewRange = 6f;

    [Range (0, 50)]
    public float AimOverviewRange = 10f;

    [Space (3)]

    [Range (0, 100)]
    public float MaximumSpread = 10f;

    [Space (3)]

    [Range (0, 50)]
    public float SpreadPerShoot = 1f;

    [Range (0, 50)]
    public float AimSpreadPerShoot = 1f;

    [Space (3)]

    [Range (0, 50)]
    public float SpreadOnMovePerShoot = 1f;

    [Range (0, 50)]
    public float SpreadOnSeatPerShoot = 1f;

    [Space (3)]

    [Range (0, 50)]
    public float SpreadRestoreSpeed = 1f;

    [Range (0, 50)]
    public float SpreadRestoreDelay = 1f;

    [Header ("Atacking")]

    [Range (0, 5000)]
    public float ReloadTimeMs = 25f;

    public float AttackCooldownMs = 500f;

    [Space (3)]

    [Range (10, 100)]
    public float ProjectileInitVelocity = 25f;

    [Range (3, 50)]
    public float ProjectileRange = 6f;

    public int ProjectileCountPerShoot = 1;

    [Range (1, 100)]
    public int DamagePerProjectile = 20;

    public int MagazineCount = 7;

    public float MeleeAttackRadius = 1f;
}
