using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_AnswerBox : MonoBehaviour
{   
    [SerializeField]
    private CanvasGroup _group = null;
    [SerializeField]
    private Transform _answerRoot = null;

    [SerializeField]
    private UI_AnswerButton _answerPrefab = null;

    public UI_AnswerButton CreateChoiceInstance(string text)
    {
        var instance = Instantiate(_answerPrefab, _answerRoot);
        instance.SetText(text);
        return instance;
    }

    public void DeleteOldAnswers()
    {
        for (int i = 0; i < _answerRoot.childCount; i++)
            Destroy(_answerRoot.GetChild(i).gameObject);
    }
}
