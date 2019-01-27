using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField]
    private NPC_Info _info = null;
    public NPC_Info Info { get { return _info; } }

    [SerializeField]
    private string _where = "";
    public string Where { get { return _where; } }

    private void Awake()
    {
        GameManager.Instance.RegisterNpc(this);
    }

    private void OnDestroy()
    {
        if(GameManager.Instance)
            GameManager.Instance.UnregisterNpc(this);
    }
}
