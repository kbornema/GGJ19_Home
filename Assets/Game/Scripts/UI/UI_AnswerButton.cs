using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_AnswerButton : MonoBehaviour
{
    [SerializeField]
    private Button _button = null;
    public Button GetButton() { return _button; }

    [SerializeField]
    private TMPro.TextMeshProUGUI _text = null;

    public void SetText(string text)
    {
        _text.text = text;
    }
}
