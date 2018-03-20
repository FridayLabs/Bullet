using UnityEngine;

public class Weapon : Equipment {
    [Range (10, 100)]
    public float ProjectileVelocity = 25f;

    [Range (3, 50)]
    public float ProjectileRange = 6f;

    [Space (10)]
    [Range (0, 10)]
    public float DefaultOverviewRange = 6f;

    [Range (10, 50)]
    public float AimOverviewRange = 10f;

    public bool ShouldReload = true;

    [Space (10)]
    [Tooltip ("Used when Should Reload is true")]
    public int MagazineCount = 7;

    [Space (10)]
    public float AttackCooldown = 1f;
}
