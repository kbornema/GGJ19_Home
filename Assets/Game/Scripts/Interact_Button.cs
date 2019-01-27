using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interact_Button : AInteractable
{
    [System.Serializable]
    public class Event : UnityEvent<EventArgs> { }

    public Event OnInteractedEvent = new Event();

    [SerializeField]
    private string _titleName = "";

    public override string GetPreviewString()
    {
        return _titleName;
    }

    public override void Activate(PlayerController player)
    {
        OnInteractedEvent.Invoke(new EventArgs(player, this));
    }

    public struct EventArgs
    {
        public PlayerController player;
        public Interact_Button button;

        public EventArgs(PlayerController player, Interact_Button button)
        {
            this.player = player;
            this.button = button;
        }
    }
}
