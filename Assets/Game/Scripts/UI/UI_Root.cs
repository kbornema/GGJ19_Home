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

    [SerializeField]
    private TextAsset _testText = null;
    [SerializeField]
    private bool _startTestText = false;

    private TextAsset _curTextAsset;
    private Story _curStory;

    public bool IsDialogueRunning { get { return _curStory != null; } }

    private string _header;

    private void Awake()
    {
        ShowDialogueUI(false);
    }

    private void Start()
    {
        if(_startTestText)
            SetDialogue("unknown", _testText);
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

    public void SetDialogue(string header, TextAsset storyJsonAsset)
    {   
        _header = header;
        ShowDialogueUI(true);
        _curTextAsset = storyJsonAsset;
        _curStory = new Story(_curTextAsset.text);
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
            UI_AnswerButton choice = CreateChoiceView("End");
            choice.GetButton().onClick.AddListener( delegate { GameManager.Instance.StopDialogue(); });
        }
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
        _curTextAsset = null;
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
