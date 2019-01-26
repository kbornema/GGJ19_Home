using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DestroyController : MonoBehaviour
{
    [System.Serializable]
    public class Event : UnityEvent<DestroyController> { }

    [SerializeField]
    private GameObject _root = null;
    public GameObject Root { get { return _root; } }

    public Event OnDestroyEvent = new Event();

    public void TriggerDestroy()
    {
        OnDestroyEvent.Invoke(this);
        Destroy(_root);
    }
}
