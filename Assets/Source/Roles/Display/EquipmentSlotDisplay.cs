using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentSlotDisplay : MonoBehaviour {

    [SerializeField] private Image image;

    [SerializeField] private UnityEngine.Color ActiveColor;
    private UnityEngine.Color inactiveColor;

    private void Awake () {
        inactiveColor = GetComponent<Image> ().color;
    }

    public void ChangeImage (Sprite sprite) {
        image.sprite = sprite;
        image.color = UnityEngine.Color.white;
    }

    public void RemoveImage () {
        image.sprite = null;
        image.color = new UnityEngine.Color (1f, 1f, 1f, 0f);
    }

    public void MarkActive (bool active) {
        GetComponent<Image> ().color = active ? ActiveColor : inactiveColor;
    }
}
