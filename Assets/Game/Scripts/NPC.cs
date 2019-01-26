using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField]
    private NPC_Info _info = null;
    public NPC_Info Info { get { return _info; } }
}
