using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactiavteInPlaymode : MonoBehaviour
{
    private void Awake()
    {
        gameObject.SetActive(false);
    }
}
