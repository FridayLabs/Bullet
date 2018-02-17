using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Bullet/Weapon")]
public class Weapon : ScriptableObject {
	public Sprite Image;
	
	public float BulletStartVelosity = 5f;
	public float BulletRange = 5f;

	public float AimArcLength = 0.2f;
	public float AimSpeed = 0.1f;

	public float MovementModifier = 0.4f;
}
