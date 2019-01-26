using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_DialogueBox : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup _group = null;
    [SerializeField]
    private TMPro.TextMeshProUGUI _titleText = null;
    [SerializeField]
    private TMPro.TextMeshProUGUI _text = null;

    public void SetText(string title, string content)
    {
        _titleText.text = title;
        _text.text = content;
    }
}
