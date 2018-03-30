using UnityEngine;

public class ActionCode {
    public KeyCode KeyCode;
    public string Name;

    public object Data;

    public static ActionCode MoveLeft = new ActionCode () { Name = "Move left", KeyCode = KeyCode.A };
    public static ActionCode MoveRight = new ActionCode () { Name = "Move right", KeyCode = KeyCode.D };
    public static ActionCode MoveUp = new ActionCode () { Name = "Move forward", KeyCode = KeyCode.W };
    public static ActionCode MoveDown = new ActionCode () { Name = "Move backward", KeyCode = KeyCode.S };

    public static ActionCode Crouch = new ActionCode () { Name = "Crouch", KeyCode = KeyCode.C };

    public static ActionCode Pickup = new ActionCode () { Name = "Pickup", KeyCode = KeyCode.E };
    public static ActionCode Drop = new ActionCode () { Name = "Drop", KeyCode = KeyCode.G };

    public static ActionCode Attack = new ActionCode () { Name = "Attack", KeyCode = KeyCode.Mouse0 };
    public static ActionCode Reload = new ActionCode () { Name = "Reload", KeyCode = KeyCode.R };
    public static ActionCode Aim = new ActionCode () { Name = "Aim", KeyCode = KeyCode.Mouse1 };

    public static ActionCode ActivateNextAmmoSlot = new ActionCode () { Name = "Activate next ammo slot", KeyCode = KeyCode.Q };

    public static ActionCode ActivateNextEquipmentSlot = new ActionCode () { Name = "Activate next equipment slot", KeyCode = KeyCode.Tab };

    public static ActionCode ActivateEquipmentSlot1 = new ActionCode () { Name = "Activate 1st equipment slot", KeyCode = KeyCode.Alpha1, Data = 1 };
    public static ActionCode ActivateEquipmentSlot2 = new ActionCode () { Name = "Activate 2nd equipment slot", KeyCode = KeyCode.Alpha2, Data = 2 };
    public static ActionCode ActivateEquipmentSlot3 = new ActionCode () { Name = "Activate 3rd equipment slot", KeyCode = KeyCode.Alpha3, Data = 3 };
    public static ActionCode ActivateEquipmentSlot4 = new ActionCode () { Name = "Activate 4th equipment slot", KeyCode = KeyCode.Alpha4, Data = 4 };
    public static ActionCode ActivateEquipmentSlot5 = new ActionCode () { Name = "Activate 5th equipment slot", KeyCode = KeyCode.Alpha5, Data = 5 };
    public static ActionCode ActivateEquipmentSlot6 = new ActionCode () { Name = "Activate 6th equipment slot", KeyCode = KeyCode.Alpha6, Data = 6 };

    public static ActionCode[] ActivateEquipmentSlotActions = {
        ActivateEquipmentSlot1,
        ActivateEquipmentSlot2,
        ActivateEquipmentSlot3,
        ActivateEquipmentSlot4,
        ActivateEquipmentSlot5,
        ActivateEquipmentSlot6,
    };
}
