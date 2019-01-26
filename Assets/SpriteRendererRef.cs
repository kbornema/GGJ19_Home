using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteRendererRef : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _spriteRenderer;
    [SerializeField]
    private Color _color;

    public void ApplyColor()
    {
        _spriteRenderer.color = _color;
    }
}
