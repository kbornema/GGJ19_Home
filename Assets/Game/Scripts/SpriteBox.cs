﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteBox : MonoBehaviour
{
#if UNITY_EDITOR

    [SerializeField]
    private BoxCollider _box = null;
    [SerializeField]
    private Vector3 _size = Vector3.one;

    [SerializeField]
    private SpriteRenderer _left = null;
    [SerializeField]
    private SpriteRenderer _right = null;
    [SerializeField]
    private SpriteRenderer _front = null;
    [SerializeField]
    private SpriteRenderer _back = null;

    [SerializeField]
    private bool _renderGizmo = true;
    [SerializeField]
    private Color _gizmoColor = Color.gray;

    private void OnValidate()
    {
        if (_left)
        {
            _left.transform.localPosition = new Vector3(-0.5f * _size.x, 0.0f, 0.0f);
            _left.size = new Vector2(_size.z, _size.y);
        }

        if (_left)
        {
            _right.transform.localPosition = new Vector3(0.5f * _size.x, 0.0f, 0.0f);
            _right.size = new Vector2(_size.z, _size.y);
        }
        if (_left)
        {
            _front.transform.localPosition = new Vector3(0.0f, 0.0f, 0.5f * _size.z);
            _front.size = new Vector2(_size.x, _size.y);
        }
        if (_left)
        {
            _back.transform.localPosition = new Vector3(0.0f, 0.0f, -0.5f * _size.z);
            _back.size = new Vector2(_size.x, _size.y);
        }

        if (_box)
        {
            _box.size = _size;
            _box.center = new Vector3(0.0f, _size.y * 0.5f, 0.0f);
        }
    }

    private void OnDrawGizmos()
    {
        if(_renderGizmo)
        {
            Gizmos.color = _gizmoColor;
            Gizmos.DrawWireCube(transform.position + new Vector3(0.0f, _size.y * 0.5f, 0.0f), _size);
        }
    }

#endif
}