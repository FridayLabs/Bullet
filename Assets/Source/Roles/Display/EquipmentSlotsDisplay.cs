using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentSlotsDisplay : MonoBehaviour {

    [SerializeField] private EquipmentSlotDisplay[] slots;

    private int currentActiveSlot = 0;

    private void Start () {
        EquipmentSlotDisplay currentSlot = slots[currentActiveSlot];
        currentSlot.MarkActive (true);
    }

    public void Equip (int slotIdx, Equipment equipment) {
        if (equipment.BagType == BagType.Default) {
            EquipmentSlotDisplay slot = slots[slotIdx];
            slot.ChangeImage (equipment.UISprite);
        }
    }

    public void Drop (int slotIdx, Equipment equipment) {
        if (equipment.BagType == BagType.Default) {
            EquipmentSlotDisplay slot = slots[slotIdx];
            slot.RemoveImage ();
        }
    }

    public void ChangeActiveSlot (Bag bag, int slotIdx) {
        if (bag.BagType != BagType.Default) {
            return;
        }
        EquipmentSlotDisplay currentSlot = slots[currentActiveSlot];
        currentSlot.MarkActive (false);

        EquipmentSlotDisplay slot = slots[slotIdx];
        slot.MarkActive (true);

        currentActiveSlot = slotIdx;
    }
}
