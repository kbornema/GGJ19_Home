using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Inventory : MonoBehaviour
{
    [SerializeField]
    private PlayerController _player;
    [SerializeField]
    private Transform _itemRoot;

    [SerializeField]
    private UI_Item _uiItemPrefab;

    private List<UI_Item> items = new List<UI_Item>();

    private void Start()
    {
        _player.AddedItemEvent.AddListener(OnItemAdded);
        _player.RemovedItemEvent.AddListener(OnItemRemoved);
    }

    private void OnItemRemoved(Item arg0)
    {   
        for (int i = items.Count - 1; i >= 0; i--)
        {
            var instance = items[i];

            if (instance.ItemOnSlot == arg0)
            {
                items.RemoveAt(i);
                Destroy(instance.gameObject);
            }
        }
    }

    private void OnItemAdded(Item arg0)
    {
        var instance = Instantiate(_uiItemPrefab, _itemRoot);
        instance.Set(arg0);
        items.Add(instance);
    }
}
