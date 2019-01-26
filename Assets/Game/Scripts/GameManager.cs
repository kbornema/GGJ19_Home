using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField]
    private UI_Root _ui = null;

    public bool IsDialogueRunning { get { return _ui.IsDialogueRunning; } }

    [SerializeField]
    private bool _mouseLocked = true;
    public bool MouseLocked { get { return _mouseLocked; } }

    private void Awake()
    {
        Instance = this;
        LockMouse(_mouseLocked);
    }

    public void StartDialogue(string header, TextAsset storyAsset)
    {
        _ui.SetDialogue(header, storyAsset);
        LockMouse(false);
    }

    public void StopDialogue()
    {
        _ui.StopDialogue();
        LockMouse(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            LockMouse(!_mouseLocked);
    }

    public void LockMouse(bool val)
    {
        _mouseLocked = val;

        if (val)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void SetInteractPreviewString(string v)
    {
        _ui.SetInteractPreviewString(v);
    }
}
