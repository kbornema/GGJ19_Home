using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact_Dialogue : AInteractable
{
    [SerializeField]
    private TextAsset _storyAsset = null;
    [SerializeField]
    private string _previewText = "";
    [SerializeField]
    private NPC _npc = null;

    public override string GetPreviewString()
    {
        if (_npc)
            return _previewText + _npc.Info.NpcName;

        return _previewText;
    }

    public override void Activate(PlayerController player)
    {
        string header = "";

        if (_npc)
            header = _npc.Info.NpcName;

        GameManager.Instance.StartDialogue(header, _storyAsset);
    }
}
