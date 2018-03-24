using NaughtyAttributes;
using UnityEngine;

public class Equipment : MonoBehaviour {

    [BoxGroup ("Equipping")]
    public BagType BagType = BagType.Default;

    [BoxGroup ("Equipping")]
    [InfoBox ("MaxStackCount: How much Player can carry objects of this type. If >1 then object is stackable", InfoBoxType.Normal, "IsStackable")]
    public int MaxStackCount;

    [BoxGroup ("UI")]
    public string FriendlyName;

    [BoxGroup ("UI")]
    [ShowAssetPreview]
    public Sprite UISprite;

    public bool IsStackable () {
        return MaxStackCount > 1;
    }

    public bool Equals (Equipment b) {
        return b != null && FriendlyName == b.FriendlyName; // TODO make this more explicit
    }
}
