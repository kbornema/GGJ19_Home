using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxColliderDebugDraw : MonoBehaviour
{
#if UNITY_EDITOR

    [SerializeField]
    private bool _render = true;
    [SerializeField]
    private BoxCollider _box = null;

    [SerializeField]
    private Color _color = Color.white;

    [SerializeField]
    private bool _asWireFrame = true;

    private void OnDrawGizmos()
    {
        if (_render && _box)
        {
            Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.lossyScale) * Matrix4x4.TRS(_box.center, Quaternion.identity, _box.size);

            if (_asWireFrame)
                Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
            else
                Gizmos.DrawCube(Vector3.zero, Vector3.one);
        }
    }

#endif
}
