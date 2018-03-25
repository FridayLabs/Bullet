using NaughtyAttributes;
using UnityEngine;

public class DataType : ScriptableObject {
#if UNITY_EDITOR
    [Multiline]
    [ResizableTextArea]
    public string DeveloperDescription = "";
#endif
}
