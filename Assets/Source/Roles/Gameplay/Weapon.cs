using UnityEngine;

public class Weapon : Equipment {
    [Space (10)]
    public GameObject ProjectilePrefab;

    [Range (10, 100)]
    public float BulletVelocity = 25f;

    [Range (3, 50)]
    public float BulletRange = 6f;

    [Space (10)]
    [Range (0, 10)]
    public float AimRange = 6f;

    [Range (10, 50)]
    public float LongAimRange = 10f;

    [Space (10)]
    public int MagazineCount = 7;

    [Space (10)]
    public float ShootCooldown = 1f;
}
