using UnityEngine;

public class Equipment : MonoBehaviour {
    [Space (10)]
    public string FriendlyName;

    [Space (10)]
    public Sprite Sprite;

    public bool IsStackable = false;

    [Tooltip ("Used only when equipment is Stackable")]
    public int StackCount = 1;

    [Tooltip ("How much Player can carry objects of this type")]
    public int MaxStackCount;
}
