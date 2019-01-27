using Ink.Runtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField]
    private PlayerController _player = null;

    public PlayerController Player { get { return _player; } }

    [SerializeField]
    private UI_Root _ui = null;

    public bool IsDialogueRunning { get { return _ui.IsDialogueRunning; } }


    [SerializeField]
    private bool _mouseLocked = true;
    public bool MouseLocked { get { return _mouseLocked; } }

    [SerializeField]
    private Color _ambientLight = Color.black;
    public Color AmbientLight { get { return _ambientLight; } set { SetAmbientLight(value); } }

    [SerializeField]
    private LayerMask _obstacleLayerMask = 0;
    public LayerMask ObstacleLayerMask { get { return _obstacleLayerMask; } }

    [SerializeField]
    private List<Item> _items = null;
    private Dictionary<string, Item> _itemDatabase;

    private void OnValidate()
    {
        SetAmbientLight(_ambientLight);
    }

    private void Awake()
    {
        Instance = this;
        LockMouse(_mouseLocked);

        _itemDatabase = new Dictionary<string, Item>();

        for (int i = 0; i < _items.Count; i++)
            _itemDatabase.Add(_items[i].name, _items[i]);
    }

    public Item GetItem(string name)
    {
        if (_itemDatabase.ContainsKey(name))
            return _itemDatabase[name];

        return null;
    }

    private void SetAmbientLight(Color value)
    {
        _ambientLight = value;
        RenderSettings.ambientLight = _ambientLight;
    }

    public void StartDialogue(GameObject owner, string header, Story story)
    {
        _ui.SetDialogue(owner, header, story);
        LockMouse(false);
    }

    public void StopDialogue()
    {
        _ui.StopDialogue();
        LockMouse(true);
    }

    public void StopDialogue(Story s)
    {
        if (_ui.IsCurrentStory(s))
            StopDialogue();
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

    public void GiveItem(string itemName)
    {
        var item = GetItem(itemName);

        if(item == null)
        {
            Debug.LogWarning("Item with name " + itemName + " does not exist!");
        }
        else
        {
            _player.GiveItem(item);
        }
    }

    private List<NPC> _npcs = new List<NPC>();

    public void RegisterNpc(NPC npc)
    {
        _npcs.Add(npc);
    }

    public void UnregisterNpc(NPC npc)
    {
        _npcs.Remove(npc);
    }

    public void EnableNpc(string who, string where, bool value)
    {
        NPC npc = GetNpc(who, where);

        if (npc)
            npc.gameObject.SetActive(value);
    }

    public NPC GetNpc(string who, string where)
    {
        for (int i = 0; i < _npcs.Count; i++)
        {
            var npc = _npcs[i];

            if(npc.Info.NpcName == who && npc.Where == where)
                return npc;
        }

        return null;
    }

    [SerializeField]
    private Checkpoint _currentCheckpoint;

    public void SetActiveCheckpoint(Checkpoint checkpoint)
    {
        _currentCheckpoint = checkpoint;
    }

    public void Respawn()
    {
        _player.Respawn(_currentCheckpoint);
    }
}
