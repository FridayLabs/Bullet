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

    public Transform DropSpawn;

    private Picker picker;

    private int activeSlotIdx = 0;
    [SerializeField] private int slotsCount = 6;
    private Equipment[] slots;

    [SerializeField] private EquipmentSlotType[] bulletsSlots;

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
        if (slots[activeSlotIdx] != null) {
            Pickable prevPickable = slots[activeSlotIdx].GetComponent<Pickable> ();
            OnDrop.Invoke (activeSlotIdx, slots[activeSlotIdx]);
            // Drop prev equipment
            prevPickable.Drop (DropSpawn);
            slots[activeSlotIdx] = null;
        }
        slots[activeSlotIdx] = equipment;
        pickable.Pick ();
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
