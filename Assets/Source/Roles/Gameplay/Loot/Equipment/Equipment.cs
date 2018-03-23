using UnityEngine;

public class Equipment : MonoBehaviour {
    [Header ("Equipping")]
    public bool ShouldBeStoredInTypedSlots = false;

    // [Tooltip ("Used only when equipment is Stackable")]
    // public int StackCount = 1;

    [Tooltip ("How much Player can carry objects of this type. If >1 then object is stackable")]
    public int MaxStackCount;

    [Header ("UI")]
    public string FriendlyName;

    public Sprite UISprite;

    public bool IsStackable () {
        return MaxStackCount > 1;
    }

    public bool Equals (Equipment b) {
        return b != null && FriendlyName == b.FriendlyName; // TODO make this more explicit
    }
}
