using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Data/Weapon")]
public class Weapon : DataType {

    public Sprite WeaponSprite;
    
    public Sprite AimSprite;

    public GameObject ProjectilePrefab;
    
    [Range(10, 100)]
    public float BulletVelocity = 25f;
    
    [Range(3, 20)]
    public float BulletRange = 6f;
    
    [Range(0, 20)]
    public float SpreadLength = 6f;

    [Range(0, 20)]
    public float SpreadLengthWhileMovement = 8f;
    
    [Range(0, 10)]
    public float AimRange = 6f;
    
    [Range(0, 5)]
    public float AimSpeed = 8f;

    public int MagazineCount = 7;

    public float ShootCooldown = 1f;
}