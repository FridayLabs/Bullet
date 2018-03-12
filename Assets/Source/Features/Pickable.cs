using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//[RequireComponent(typeof(SpriteRenderer))]
public class Pickable : MonoBehaviour {
    [SerializeField] private Equipment PickupItem;
//    private SpriteRenderer spriteRenderer;

    public UnityEvent OnHighlight, OnDehighlight;

//    public UnityEngine.Color HighlightColor;
    
//    void OnEnable() {
//        spriteRenderer = GetComponent<SpriteRenderer>();
//    }

    public void Highlight() {
        OnHighlight.Invoke();
//        UpdateOutline(true);
    }

    public void Dehighlight() {
        OnDehighlight.Invoke();
//        UpdateOutline(false);
    }
//
//    void UpdateOutline(bool outline) {
//        MaterialPropertyBlock mpb = new MaterialPropertyBlock();
//        spriteRenderer.GetPropertyBlock(mpb);
//        mpb.SetFloat("_Outline", outline ? 1f : 0);
//        mpb.SetColor("_OutlineColor", HighlightColor);
//        spriteRenderer.SetPropertyBlock(mpb);
//    }
}