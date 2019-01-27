using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Item : ScriptableObject
{
    public string DisplayName = "Unbekannt";
    public Sprite ItemIcon = null;
    public string InkName = "inkName";
}
