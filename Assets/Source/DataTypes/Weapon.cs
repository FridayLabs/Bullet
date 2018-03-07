using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Data/Weapon")]
public class Weapon : DataType {

    public Sprite WeaponSprite;
    
    public Sprite AimSprite;

    public GameObject ProjectilePrefab;
    
    [Range(10, 100)]
    public float BulletVelocity = 25f;
    
    [Range(3, 50)]
    public float BulletRange = 6f;
    
    [Range(0, 10)]
    public float AimRange = 6f;
    
    [Range(10, 50)]
    public float LongAimRange = 10f;
    
    public int MagazineCount = 7;

    public float ShootCooldown = 1f;
}