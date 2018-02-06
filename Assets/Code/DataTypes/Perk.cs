using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Perk", menuName = "Bullet/Perk")]
public class Perk : ScriptableObject {
	public float MovementSpeed = 1.5f;

	public Weapon weapon;

	public Sprite Sprite;
}
