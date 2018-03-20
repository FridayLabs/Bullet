using UnityEngine;

public class Equipment : MonoBehaviour {
    [Space (10)]
    public string FriendlyName;

    [Space (10)]
    public Sprite UISprite;

    public bool ShouldBeStoredInTypedSlots = false;

    [Tooltip ("Used only when equipment is Stackable")]
    public int StackCount = 1;

    [Tooltip ("How much Player can carry objects of this type. If >1 then object is stackable")]
    public int MaxStackCount;

    public bool IsStackable () {
        return MaxStackCount > 1;
    }

    public bool Equals (Equipment b) {
        return b != null && FriendlyName == b.FriendlyName; // TODO make this more explicit
    }
}
