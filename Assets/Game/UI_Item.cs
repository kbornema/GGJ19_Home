using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Item : MonoBehaviour
{
    [SerializeField]
    private Image _image = null;
    public Image GetImage() { return _image; }
    
    public Item ItemOnSlot;

    public void Set(Item arg0)
    {
        _image.sprite = arg0.ItemIcon;
        ItemOnSlot = arg0;
    }
}
