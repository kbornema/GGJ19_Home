using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : AInteractable
{
    [SerializeField]
    private string _previewString = "Gitter";

    [SerializeField]
    private bool _isLocked = false;
    public bool IsLocked { get { return _isLocked; } set { _isLocked = value; } }

    [SerializeField]
    private bool _isOpen = false;

    [SerializeField]
    private Transform _root;

    [SerializeField]
    private Transform _target = null;

    [SerializeField]
    private float _time = 1.0f;
    private float _curTime = 0.0f;

    [SerializeField]
    private AnimationCurve _moveCurve = null;

    private Vector3 _defaultPos;
    private Vector3 _targetPos;

    [SerializeField]
    private bool _isMoving = false;

    public override string GetPreviewString()
    {
        if (_isMoving)
            return "";

        return _previewString;
    }

    private void Start()
    {
        _defaultPos = transform.position;
        _targetPos = _target.position;

        if(_isOpen)
            _root.position = _targetPos;
    }

    public override void Activate(PlayerController player)
    {
        if(_isLocked || _isMoving)
            return;

        Open();
    }

    public void ForceOpen()
    {
        _isLocked = false;
        Open();
    }

    private void Open()
    {
        _curTime = 0.0f;
        _isMoving = true;
    }

    private void Update()
    {
        if(_isMoving)
        {
            _curTime += Time.deltaTime;

            float t = Mathf.Clamp01(_curTime / _time);  
            float evalT = t;

            if (_isOpen)
                evalT = 1.0f - evalT;

            Vector3 curPos = Vector3.Lerp(_defaultPos, _targetPos, _moveCurve.Evaluate(evalT));

            _root.position = curPos;

            if (t >= 1.0f)
            {
                _isMoving = false;
                _isOpen = !_isOpen;
            }
        }
    }
}
