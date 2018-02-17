using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Bullet/Weapon")]
public class Weapon : ScriptableObject {
	public Sprite Image;
	
	public float BulletStartVelosity = 5f;
	public float BulletRange = 5f;

	[Range(3, 13)]
	public float AimArcLength = 3f;
	
	[Range(0, 2)]
	public float AimSpeed = 1f;

	[Range(0, 3)]
	public float MovementModifier = 1f;
}
