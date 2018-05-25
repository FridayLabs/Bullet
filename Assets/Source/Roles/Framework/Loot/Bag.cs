using System;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

[System.Serializable]
public class Bag {
    public BagType BagType;

    [SerializeField]
    [ReadOnly]
    private int activeSlotIdx;

    [System.Serializable]
    class Slot {
        public Equipment Equipment;
        public int Count = 0;
        public Slot (Equipment equipment, int count) {
            this.Equipment = equipment;
            this.Count = count;
        }

        public bool IsEmpty () {
            return Equipment == null || Count == 0;
        }
    }

    [SerializeField]
    [ReadOnly]
    private List<Slot> slots = new List<Slot> ();
    private Equipper equipper;

    public void SetEquipper (Equipper equipper) {
        this.equipper = equipper;
    }

    public void Add (Equipment equipment, int count) {
        int slotIdx = findEquipSlot (equipment, activeSlotIdx);
        if (!isSlotEmpty (slotIdx)) {
            bool shouldStack = equipment.Equals (slots[slotIdx].Equipment) &&
                slots[slotIdx].Equipment.IsStackable () &&
                equipment.IsStackable ();

            if (shouldStack) {
                // stacking
                int take = calculateEquipCount (count, slots[slotIdx].Count, equipment.MaxStackCount);
                if (take > 0) {
                    slots[slotIdx].Count += take;
                    equipper.ProcessEquipEvents (this, slotIdx, equipment, take);
                }
                return;
            } else {
                Drop (slotIdx);
            }
        }
        if (isSlotEmpty (slotIdx)) {
            Slot slot = new Slot (equipment, count);
            slots[slotIdx] = slot;
        }
        equipper.ProcessEquipEvents (this, slotIdx, equipment, count);
    }
    public void Drop (int slotIdx) {
        if (isSlotEmpty (slotIdx)) {
            return;
        }
        Slot slot = slots[slotIdx];
        Equipment equipment = slots[slotIdx].Equipment;
        int count = slots[slotIdx].Count;
        slots[slotIdx].Equipment = null;
        slots[slotIdx].Count = 0;
        equipper.ProcessDropEvents (this, slotIdx, equipment, count);
    }

    public void DropFromActiveSlot () {
        Drop (activeSlotIdx);
    }

    public Equipment GetActiveEquipment () {
        return !isSlotEmpty (activeSlotIdx) ? slots[activeSlotIdx].Equipment : null;
    }

    public int GetEquipmentCount (Equipment equipment) {
        if (equipment == null) {
            return 0;
        }
        int slotIdx = findEquipSlot (equipment, -1);
        return (slotIdx != -1) ? slots[slotIdx].Count : 0;
    }

    public void SetEquipmentCount (Equipment equipment, int count) {
        int slotIdx = findEquipSlot (equipment, -1);
        if (slotIdx != -1) {
            slots[slotIdx].Count = count;
        }
    }

    private bool isSlotEmpty (int slotIdx) {
        return slots[slotIdx] == null || slots[slotIdx].IsEmpty ();
    }

    private int findEquipSlot (Equipment equipment, int defaultSlotIdx) {
        int equipSlotIdx = defaultSlotIdx;
        if (equipment.IsStackable ()) {
            for (int i = 0; i < slots.Count; i++) {
                if (slots[i] != null && !slots[i].IsEmpty () && equipment.Equals (slots[i].Equipment)) {
                    return i;
                }
            }
        }
        return equipSlotIdx;
    }

    public void ActivateNextSlot () {
        ActivateSlot ((activeSlotIdx + 1) % slots.Count);
    }

    public void ActivateSlot (int idx) {
        if (idx < slots.Count) {
            activeSlotIdx = idx;
            equipper.ProcessChangeActiveSlotEvents (this, idx);
        } else {
            throw new Exception ("Index " + idx + " is too large for player's slots array in bag " + BagType);
        }
    }

    public bool CanCarryMoreOf (Equipment equipment) {
        Pickable pickable = equipment.GetComponent<Pickable> ();
        int slotIdx = findEquipSlot (equipment, activeSlotIdx);
        return calculateEquipCount (pickable.StackCount, slots[slotIdx].Count, equipment.MaxStackCount) > 0;
    }

    protected int calculateEquipCount (int equipCount, int currentCount, int maxCount) {
        if (currentCount < maxCount) {
            int d = maxCount - currentCount;
            int take = (d > equipCount) ? equipCount : d;
            if (take > 0) {
                return take;
            }
        }
        return 0;
    }
}
