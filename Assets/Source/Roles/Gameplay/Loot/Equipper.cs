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
    private Equipment[] slots;

    [System.Serializable]
    struct BulletSlot {
        public EquipmentSlotType SlotType;
        public int StackCount;
    }

    [SerializeField] private BulletSlot[] bulletsSlots;

    private void Start () {
        picker = GetComponent<Picker> ();
        slots = new Equipment[slotsCount];
    }

    private void LateUpdate () {
        if (Input.GetKeyDown (KeyCode.E)) { // TODO
            Pickable pickable = picker.GetHighlightedPickable ();
            if (pickable) {
                equip (pickable);
            }
        }

        if (Input.GetKeyDown (KeyCode.Q)) { // TODO next active slot
            activateSlot ((activeSlotIdx + 1) % slotsCount);
        }
    }

    private void equip (Pickable pickable) {
        Equipment equipment = pickable.GetEquipment ();

        // equip into bullets slots
        if (typeof (BulletsPack) == equipment.GetType ()) {
            BulletsPack bp = equipment as BulletsPack;
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
            return;
        }

        if (slots[activeSlotIdx] != null) {
            Pickable prevPickable = slots[activeSlotIdx].GetComponent<Pickable> ();
            OnDrop.Invoke (activeSlotIdx, slots[activeSlotIdx]);
            picker.Drop (prevPickable, equipment.StackCount);
            slots[activeSlotIdx] = null;
        }
        slots[activeSlotIdx] = equipment;
        picker.Pick (pickable, equipment.StackCount);
        OnEquip.Invoke (activeSlotIdx, equipment);
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
