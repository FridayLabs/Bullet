using System;
using UnityEngine;

[AttributeUsage (AttributeTargets.Field, Inherited = true, AllowMultiple = true)]
public class TestButtonAttribute : PropertyAttribute {
    public string buttonLabel;
    public string methodName;

    // Set this false to make the button not work whilst in playmode
    public bool isActiveAtRuntime = true;
    // Set this to false to make the button not work when the game isnt running
    public bool isActiveInEditor = true;

    public TestButtonAttribute (string buttonLabel, string methodName, int order = 1) {
        this.buttonLabel = buttonLabel;
        this.methodName = methodName;

        this.order = order; // Defualt the order to 1 so this can draw under headder attribles
    }
}
