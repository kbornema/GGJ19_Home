using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class NPC_Info : ScriptableObject
{
    [SerializeField]
    private string _npcName = "Gerd";
    public string NpcName { get { return _npcName; } }
}
