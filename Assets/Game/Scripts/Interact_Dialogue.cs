using Ink.Runtime;
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

    private Story _story;

    [SerializeField]
    private GameObject _root = null;

    [SerializeField]
    private List<AInteractable> _interactables = null;

    [SerializeField]
    private string _customInkString = "";

    public override string GetPreviewString()
    {
        if (_npc)
            return _previewText + _npc.Info.NpcName;

        return _previewText;
    }

    public override void Activate(PlayerController player)
    {
        if (_storyAsset == null)
            return;

        string header = "";

        if (_npc)
            header = _npc.Info.NpcName;

        if(_story == null)
            _story = new Story(_storyAsset.text);

        var variables = _story.variablesState;

        if(variables.HasVariable("string_objectText"))
        {
            variables["string_objectText"] = _customInkString;
        }
   

        GameManager.Instance.StartDialogue(_root, header, _story, _interactables);
    }

    private void OnDestroy()
    {
        if(GameManager.Instance)
        {
            GameManager.Instance.StopDialogue(_story);
        }
    }
}
