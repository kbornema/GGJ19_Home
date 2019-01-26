using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact_TorchToggle : AInteractable
{
    [SerializeField]
    private GameObject _onObject = null;

    [SerializeField]
    private bool _isOn = true;

    private void OnValidate()
    {
        ActivateTorch(_isOn);
    }

    private void ActivateTorch(bool val)
    {
        _isOn = val;

        if (_onObject)
            _onObject.SetActive(val);
    }

    public override void Activate(PlayerController player)
    {
        ActivateTorch(!_isOn);
    }

    public override string GetPreviewString()
    {
        return "Fackel";
    }
}
