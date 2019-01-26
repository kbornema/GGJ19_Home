using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AInteractable : MonoBehaviour
{
    public virtual string GetPreviewString() { return ""; }
    public abstract void Activate(PlayerController player);
}
