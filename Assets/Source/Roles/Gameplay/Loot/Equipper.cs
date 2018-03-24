using System;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent (typeof (Picker))]
public class Equipper : MonoBehaviour {

    public List<Bag> Bags = new List<Bag> ();

    [System.Serializable]
    public class EquipEvent : UnityEvent<int, Equipment> { }

    [System.Serializable]
    public class ActiveSlotEvent : UnityEvent<Bag, int> { }

    public Weapon DefaultWeapon;

    public EquipEvent OnEquip, OnDrop;
    public ActiveSlotEvent OnChangeActiveSlot;

    private Picker picker;

    private void Start () {
        picker = GetComponent<Picker> ();

        foreach (Bag bag in Bags) {
            bag.SetEquipper (this);
        }
    }

    private void LateUpdate () {
        Pickable pickable = picker.GetHighlightedPickable ();
        if (pickable) {
            Equipment equipment = pickable.GetEquipment ();
            if (pickable.AutoPickup || Input.GetKeyDown (KeyCode.E)) { // TODO
                findBagByType (equipment.BagType).Add (equipment, pickable.StackCount);
            }
        }

        // TODO INPUT MANAGEEEEEEEEEER
        if (Input.GetKeyDown (KeyCode.Q)) {
            findBagByType (BagType.Ammo).ActivateNextSlot ();
        }
        // TODO INPUT MANAGEEEEEEEEEER
        if (Input.GetKeyDown (KeyCode.Alpha1)) {
            findBagByType (BagType.Ammo).ActivateSlot (0);
        }
        // TODO INPUT MANAGEEEEEEEEEER
        if (Input.GetKeyDown (KeyCode.Alpha2)) {
            findBagByType (BagType.Ammo).ActivateSlot (1);
        }
        // TODO INPUT MANAGEEEEEEEEEER
        if (Input.GetKeyDown (KeyCode.Alpha3)) {
            findBagByType (BagType.Ammo).ActivateSlot (2);
        }
        // TODO INPUT MANAGEEEEEEEEEER
        if (Input.GetKeyDown (KeyCode.Alpha4)) {
            findBagByType (BagType.Ammo).ActivateSlot (3);
        }
        // TODO INPUT MANAGEEEEEEEEEER
        if (Input.GetKeyDown (KeyCode.Alpha5)) {
            findBagByType (BagType.Ammo).ActivateSlot (4);
        }
        // TODO INPUT MANAGEEEEEEEEEER
        if (Input.GetKeyDown (KeyCode.Alpha5)) {
            findBagByType (BagType.Ammo).ActivateSlot (6);
        }

        if (Input.GetKeyDown (KeyCode.G)) { // TODO Drop
            findBagByType (BagType.Default).DropFromActiveSlot ();
        }
    }

    private Bag findBagByType (BagType type) {
        foreach (Bag bag in Bags) {
            if (type == bag.BagType) {
                return bag;
            }
        }
        throw new Exception ("Player doesn't have bag with type " + type);
    }

    public Weapon GetActiveWeapon () {
        Equipment equipment = findBagByType (BagType.Default).GetActiveEquipment ();
        if (equipment && equipment.GetType () == typeof (Weapon)) {
            return equipment as Weapon;
        }
        return DefaultWeapon;
    }

    public Ammo GetActiveAmmo () {
        return findBagByType (BagType.Ammo).GetActiveEquipment () as Ammo;
    }

    public bool CanCarryMoreOf (Equipment equipment) {
        return findBagByType (equipment.BagType).CanCarryMoreOf (equipment);
    }

    public void ProcessEquipEvents (Bag bag, int slotIdx, Equipment equipment, int count) {
        picker.Pick (equipment.GetComponent<Pickable> (), count);
        OnEquip.Invoke (slotIdx, equipment);
    }

    public void ProcessDropEvents (Bag bag, int slotIdx, Equipment equipment, int count) {
        OnDrop.Invoke (slotIdx, equipment);
        picker.Drop (equipment.GetComponent<Pickable> (), count);
    }

    public void ProcessChangeActiveSlotEvents (Bag bag, int slotIdx) {
        OnChangeActiveSlot.Invoke (bag, slotIdx);
    }
}
