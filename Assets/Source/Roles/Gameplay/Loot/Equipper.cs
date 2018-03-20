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

    public EquipEvent OnEquip, OnDrop;
    public ActiveSlotEvent OnChangeActiveSlot;

    private Picker picker;

    private int activeSlotIdx = 0;
    [SerializeField] private int slotsCount = 6;

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

    [SerializeField]
    private EquipmentSlot[] slots;

    [System.Serializable]
    struct BulletSlot {
        public EquipmentSlotType SlotType;
        public int StackCount;
    }

    [SerializeField] private BulletSlot[] bulletsSlots;

    private void Start () {
        picker = GetComponent<Picker> ();
        slots = new EquipmentSlot[slotsCount];
    }

    private void LateUpdate () {
        if (Input.GetKeyDown (KeyCode.E)) { // TODO
            Pickable pickable = picker.GetHighlightedPickable ();
            if (pickable) {
                Equipment pickupEquipment = pickable.GetEquipment ();
                if (typeof (BulletsPack) == pickupEquipment.GetType ()) {
                    equipBullets (pickable);
                } else {
                    equip (pickable);
                }
            }
        }

        if (Input.GetKeyDown (KeyCode.Q)) { // TODO next active slot
            activateSlot ((activeSlotIdx + 1) % slotsCount);
        }
    }

    private void equip (Pickable pickable) {
        Equipment pickupEquipment = pickable.GetEquipment ();
        int slotIdx = findEquipSlot (pickupEquipment);

        if (!isSlotEmpty (slotIdx)) {
            bool shouldStack = pickupEquipment.Equals (slots[slotIdx].Equipment) &&
                slots[slotIdx].Equipment.IsStackable () &&
                pickupEquipment.IsStackable ();

            if (!shouldStack) {
                Pickable equippedPickable = slots[slotIdx].Equipment.GetComponent<Pickable> ();
                OnDrop.Invoke (slotIdx, slots[slotIdx].Equipment);
                picker.Drop (equippedPickable, slots[slotIdx].StackCount);
                slots[slotIdx] = null;
            } else {
                slots[slotIdx].StackCount += pickupEquipment.StackCount;
            }
        }

        if (isSlotEmpty (slotIdx)) {
            slots[slotIdx] = new EquipmentSlot (pickupEquipment, pickupEquipment.StackCount);
        }
        picker.Pick (pickable, pickupEquipment.StackCount);
        OnEquip.Invoke (slotIdx, pickupEquipment);
    }

    private bool isSlotEmpty (int slotIdx) {
        return slots[slotIdx] == null || slots[slotIdx].IsEmpty ();
    }

    private void equipBullets (Pickable pickable) {
        BulletsPack bp = pickable.GetEquipment () as BulletsPack;
        for (int i = 0; i < bulletsSlots.Length; i++) {
            BulletSlot slot = bulletsSlots[i];
            if (slot.SlotType == bp.SlotType) {
                if (slot.StackCount < bp.MaxStackCount) { // carry less then can
                    int d = bp.MaxStackCount - slot.StackCount;
                    int take = (d > bp.StackCount) ? bp.StackCount : d;
                    if (take > 0) {
                        picker.Pick (pickable, take);
                        bulletsSlots[i].StackCount += take;
                    }
                }
            }
        }
    }

    private int findEquipSlot (Equipment pickupEquipment) {
        int equipSlotIdx = activeSlotIdx;
        if (pickupEquipment.IsStackable ()) {
            for (int i = 0; i < slots.Length; i++) {
                if (slots[i] != null && !slots[i].IsEmpty () && pickupEquipment.Equals (slots[i].Equipment)) {
                    return i;
                }
            }
        }
        return equipSlotIdx;
    }

    private void activateSlot (int idx) {
        if (idx < slotsCount) {
            activeSlotIdx = idx;
            OnChangeActiveSlot.Invoke (idx);
        } else {
            Debug.LogWarning ("Index " + idx + " is too large for player's slots array");
        }
    }
}
