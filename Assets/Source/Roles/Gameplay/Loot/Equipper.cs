using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent (typeof (Picker))]
public class Equipper : MonoBehaviour {

    [System.Serializable]
    public class EquipEvent : UnityEvent<int, Equipment> { }

    [System.Serializable]
    public class ActiveSlotEvent : UnityEvent<int> { }

    [System.Serializable]
    class EquipmentSlot {
        public Equipment Equipment;
        public int StackCount = 0;
        public EquipmentSlot (Equipment equipment, int stackCount) {
            this.Equipment = equipment;
            this.StackCount = stackCount;
        }

        public bool IsEmpty () {
            return Equipment == null || StackCount == 0;
        }
    }

    [System.Serializable]
    class TypedEquipmentSlot : EquipmentSlot {
        public Equipment Type;
        public TypedEquipmentSlot (Equipment type, Equipment equipment, int stackCount) : base (equipment, stackCount) {
            this.Type = type;
        }
    }

    public Equipment DefaultEquipment;

    [SerializeField]
    private List<EquipmentSlot> slots = new List<EquipmentSlot> ();

    [SerializeField]
    private List<TypedEquipmentSlot> typedSlots = new List<TypedEquipmentSlot> ();

    public EquipEvent OnEquip, OnDrop;
    public ActiveSlotEvent OnChangeActiveSlot;

    private Picker picker;

    private int activeSlotIdx = 0;

    private void Start () {
        picker = GetComponent<Picker> ();
    }

    private void LateUpdate () {
        Pickable pickable = picker.GetHighlightedPickable ();
        if (pickable) {
            Equipment pickupEquipment = pickable.GetEquipment ();
            if (pickable.AutoPickup || Input.GetKeyDown (KeyCode.E)) { // TODO
                if (pickupEquipment.ShouldBeStoredInTypedSlots) {
                    equipTyped (pickable);
                } else {
                    equip (pickable);
                }
            }
        }

        if (Input.GetKeyDown (KeyCode.Q)) { // TODO next active slot
            activateSlot ((activeSlotIdx + 1) % slots.Count);
        }

        if (Input.GetKeyDown (KeyCode.G)) { // TODO Drop
            dropFrom (slots, activeSlotIdx);
        }
    }

    public Equipment GetActiveEquipment () {
        return !isSlotEmpty (slots, activeSlotIdx) ? slots[activeSlotIdx].Equipment : DefaultEquipment;
    }

    public Weapon GetActiveWeapon () {
        Equipment equipment = GetActiveEquipment ();
        if (equipment.GetType () == typeof (Weapon)) {
            return equipment as Weapon;
        }
        return null;
    }

    public bool CanCarryMoreOf (Equipment equipment) {
        if (equipment.ShouldBeStoredInTypedSlots) {
            int slotIdx = findTypedEquipSlot (typedSlots, equipment);
            return calculateStackEquipCount (equipment, typedSlots[slotIdx].StackCount) > 0;
        } else {
            int slotIdx = findEquipSlot (slots, equipment, activeSlotIdx);
            return calculateStackEquipCount (equipment, slots[slotIdx].StackCount) > 0;
        }
    }

    private void equipTyped (Pickable pickable) {
        Equipment pickupEquipment = pickable.GetEquipment ();
        int slotIdx = findTypedEquipSlot (typedSlots, pickupEquipment);
        if (slotIdx == -1) {
            return;
        }
        if (typedSlots[slotIdx] != null && !typedSlots[slotIdx].IsEmpty ()) {
            bool shouldStack = pickupEquipment.Equals (typedSlots[slotIdx].Equipment) &&
                typedSlots[slotIdx].Equipment.IsStackable () &&
                pickupEquipment.IsStackable ();

            if (shouldStack) {
                // stacking
                int take = calculateStackEquipCount (pickupEquipment, typedSlots[slotIdx].StackCount);
                if (take > 0) {
                    OnEquip.Invoke (slotIdx, pickupEquipment);
                    picker.Pick (pickable, take);
                    typedSlots[slotIdx].StackCount += take;
                }
                return;
            }
        }

        if (typedSlots[slotIdx] == null || typedSlots[slotIdx].IsEmpty ()) {
            typedSlots[slotIdx].Equipment = pickupEquipment;
            typedSlots[slotIdx].StackCount = pickupEquipment.StackCount;
        }
        picker.Pick (pickable, pickupEquipment.StackCount);
        OnEquip.Invoke (slotIdx, pickupEquipment);
    }

    private void equip (Pickable pickable) {
        Equipment pickupEquipment = pickable.GetEquipment ();
        int slotIdx = findEquipSlot (slots, pickupEquipment, activeSlotIdx);

        if (!isSlotEmpty (slots, slotIdx)) {
            bool shouldStack = pickupEquipment.Equals (slots[slotIdx].Equipment) &&
                slots[slotIdx].Equipment.IsStackable () &&
                pickupEquipment.IsStackable ();

            if (shouldStack) {
                // stacking
                int take = calculateStackEquipCount (pickupEquipment, slots[slotIdx].StackCount);
                if (take > 0) {
                    OnEquip.Invoke (slotIdx, pickupEquipment);
                    picker.Pick (pickable, take);
                    slots[slotIdx].StackCount += take;
                }
                return;
            } else {
                dropFrom (slots, slotIdx);
            }
        }
        if (isSlotEmpty (slots, slotIdx)) {
            EquipmentSlot slot = new EquipmentSlot (pickupEquipment, pickupEquipment.StackCount);
            slots[slotIdx] = slot;
        }
        picker.Pick (pickable, pickupEquipment.StackCount);
        OnEquip.Invoke (slotIdx, pickupEquipment);
    }

    protected int calculateStackEquipCount (Equipment equipment, int currentStackCount) {
        if (currentStackCount < equipment.MaxStackCount) {
            int d = equipment.MaxStackCount - currentStackCount;
            int take = (d > equipment.StackCount) ? equipment.StackCount : d;
            if (take > 0) {
                return take;
            }
        }
        return 0;
    }

    private void dropFrom (List<EquipmentSlot> slots, int slotIdx) {
        if (isSlotEmpty (slots, slotIdx)) {
            return;
        }
        EquipmentSlot slot = slots[slotIdx];
        Equipment equipment = slots[slotIdx].Equipment;
        slots[slotIdx].Equipment = null;
        slots[slotIdx].StackCount = 0;
        OnDrop.Invoke (slotIdx, equipment);
        picker.Drop (equipment.GetComponent<Pickable> (), slot.StackCount);
    }

    private bool isSlotEmpty (List<EquipmentSlot> slots, int slotIdx) {
        return slots[slotIdx] == null || slots[slotIdx].IsEmpty ();
    }

    private int findEquipSlot (List<EquipmentSlot> slots, Equipment pickupEquipment, int defaultSlotIdx) {
        int equipSlotIdx = defaultSlotIdx;
        if (pickupEquipment.IsStackable ()) {
            for (int i = 0; i < slots.Count; i++) {
                if (slots[i] != null && !slots[i].IsEmpty () && pickupEquipment.Equals (slots[i].Equipment)) {
                    return i;
                }
            }
        }
        return equipSlotIdx;
    }

    private int findTypedEquipSlot (List<TypedEquipmentSlot> slots, Equipment pickupEquipment) {
        for (int i = 0; i < slots.Count; i++) {
            if (slots[i].Type && slots[i].Type.Equals (pickupEquipment)) {
                return i;
            }
        }
        return -1;
    }

    private void activateSlot (int idx) {
        if (idx < slots.Count) {
            activeSlotIdx = idx;
            OnChangeActiveSlot.Invoke (idx);
        } else {
            Debug.LogWarning ("Index " + idx + " is too large for player's slots array");
        }
    }
}
