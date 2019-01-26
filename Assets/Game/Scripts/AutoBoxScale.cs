using UnityEngine;

public class AutoBoxScale : MonoBehaviour
{
    [SerializeField]
    private BoxCollider _box = null;

    [SerializeField]
    private SpriteRenderer _spriteRenderer = null;

    [SerializeField]
    private Vector3 _size = new Vector3(1.0f, 1.0f, 0.2f);

    [SerializeField]
    private Vector3 _sizeCenterScale = new Vector3(0.0f, 0.5f, 0.0f);
    [SerializeField]
    private Vector3 _center = new Vector3(0.0f, 0.0f, 0.0f);

    private void Reset()
    {
        if (!_box)
            _box = GetComponent<BoxCollider>();

        if (!_spriteRenderer)
            _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnValidate()
    {
        if (_spriteRenderer)
            _spriteRenderer.size = new Vector2(_size.x, _size.y);

        if (_box)
        {
            Vector3 center = _size;
            center.Scale(_sizeCenterScale);
            center += _center;

            _box.center = center;
            _box.size = _size;
        }
    }

}
