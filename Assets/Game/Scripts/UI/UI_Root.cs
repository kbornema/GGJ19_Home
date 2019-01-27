using Ink.Runtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Root : MonoBehaviour
{   
    [SerializeField]
    private UI_DialogueBox _npcDialogueBox = null;
    [SerializeField]
    private UI_AnswerBox _answerBox = null;
    [SerializeField]
    private TMPro.TextMeshProUGUI _previewString = null;

    private Story _curStory;

    public bool IsDialogueRunning { get { return _curStory != null; } }

    private string _header;

    private GameObject _dialogueOwner;

    private void Awake()
    {
        ShowDialogueUI(false);
    }

    public void SetInteractPreviewString(string s)
    {
        if (s == null || s.Length == 0)
            _previewString.gameObject.SetActive(false);

        else
        {
            _previewString.gameObject.SetActive(true);
            _previewString.text = s;
        }
    }

    private void AddFunctions(Story story)
    {
        const string HAS_ITEM_FUNCTION_NAME = "hasItem";

        if (!_curStory.HasExternalFunctionBound(HAS_ITEM_FUNCTION_NAME))
        {
            _curStory.BindExternalFunction(HAS_ITEM_FUNCTION_NAME, (string itemName) =>
            {
                return GameManager.Instance.Player.HasItem(GameManager.Instance.GetItem(itemName));
            });
        }

        const string REMOVE_ITEM_FUNCTION_NAME = "removeItem";

        if (!_curStory.HasExternalFunctionBound(REMOVE_ITEM_FUNCTION_NAME))
        {
            _curStory.BindExternalFunction(REMOVE_ITEM_FUNCTION_NAME, (string itemName) =>
            {
                GameManager.Instance.Player.RemoveItem(GameManager.Instance.GetItem(itemName));
            });
        }

        const string ADD_ITEM_FUNCTION_NAME = "giveItem";

        if (!_curStory.HasExternalFunctionBound(ADD_ITEM_FUNCTION_NAME))
        {
            _curStory.BindExternalFunction(ADD_ITEM_FUNCTION_NAME, (string itemName) =>
            {
                GameManager.Instance.Player.GiveItem(GameManager.Instance.GetItem(itemName));
            });
        }

        const string ACTIVATE_NPC = "enableNpc";

        if (!_curStory.HasExternalFunctionBound(ACTIVATE_NPC))
        {
            _curStory.BindExternalFunction(ACTIVATE_NPC, (string who, string where, bool value) =>
            {
                GameManager.Instance.EnableNpc(who, where, value);
            });
        }

        const string DESTROY_DIALOGUE_OWNER_FUNCTION_NAME = "destroyDialogueOwner";

        if (!_curStory.HasExternalFunctionBound(DESTROY_DIALOGUE_OWNER_FUNCTION_NAME))
        {
            _curStory.BindExternalFunction(DESTROY_DIALOGUE_OWNER_FUNCTION_NAME, () =>
            {   
                if(_dialogueOwner)
                {
                    var destroyFoo = _dialogueOwner.GetComponent<DestroyController>();

                    if(destroyFoo)
                        destroyFoo.TriggerDestroy();

                    else
                        Destroy(_dialogueOwner);
                }
            });
        }

    }

    public bool IsCurrentStory(Story s)
    {
        return _curStory == s;
    }

    public void SetDialogue(GameObject owner, string header, Story story)
    {
        _dialogueOwner = owner;
        _curStory = story;
        AddFunctions(_curStory);
        _curStory.ChoosePathString("startKnot");

        _header = header;
        ShowDialogueUI(true);
        RefreshView();
    }

    private void RefreshView()
    {   
        _answerBox.DeleteOldAnswers();

        while (_curStory.canContinue)
        {
            // Continue gets the next line of the story
            string text = _curStory.Continue();

            // This removes any white space from the text.
            text = text.Trim();
            // Display the text on screen!
            CreateContentView(_header, text);
        }

        // Display all the choices, if there are any!
        if (_curStory.currentChoices.Count > 0)
        {
            for (int i = 0; i < _curStory.currentChoices.Count; i++)
            {
                Choice choice = _curStory.currentChoices[i];
                UI_AnswerButton button = CreateChoiceView(choice.text.Trim());
                // Tell the button what to do when we press it
                button.GetButton().onClick.AddListener( delegate { OnClickChoiceButton(choice); });
            }
        }
        // If we've read all the content and there's no choices, the story is finished!
        else
        {
            GameManager.Instance.StopDialogue();
            //UI_AnswerButton choice = CreateChoiceView("End");
            //choice.GetButton().onClick.AddListener( delegate {  });
        }
    }

    private void GiveItem(string item)
    {
        GameManager.Instance.GiveItem(item);
    }

    private UI_AnswerButton CreateChoiceView(string text)
    {   
        return _answerBox.CreateChoiceInstance(text);
    }

    private void ShowDialogueUI(bool value)
    {
        _npcDialogueBox.gameObject.SetActive(value);
        _answerBox.gameObject.SetActive(value);
    }

    public void StopDialogue()
    {
        _answerBox.DeleteOldAnswers();
        _curStory = null;
        ShowDialogueUI(false);
    }

    private void OnClickChoiceButton(Choice choice)
    {
        _curStory.ChooseChoiceIndex(choice.index);
        RefreshView();
    }

    private void CreateContentView(string header, string text)
    {
        _npcDialogueBox.SetText(header, text);
    }
}
