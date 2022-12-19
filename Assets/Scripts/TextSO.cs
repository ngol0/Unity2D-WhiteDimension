using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TextEntry
{
    public int index;
    [TextArea(3,10)] public string textEntry;
}

[CreateAssetMenu(menuName = "Text SO")]
public class TextSO : ScriptableObject
{
    public List<TextEntry> textEntries;
}
