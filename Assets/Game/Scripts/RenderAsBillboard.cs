using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> 
/// Makes a <see cref="GameObject"/> face the rendering <see cref="Camera"/>. 
/// Only works on <see cref="GameObject"/> that have a <see cref="Renderer"/> component attached and if it's visible.
/// </summary>
[ExecuteInEditMode]
public class RenderAsBillboard : MonoBehaviour
{

    /// <summary> What Camera property to face when rendering. </summary>
    public enum Mode
    {
        /// <summary> Rotates towards the camera position. </summary>
        Position,
        /// <summary> Rotates so that the forward vectors match. </summary>
        Rotation
    }

    //public Mode BillboardMode = Mode.Position;
    /// <summary> Used to restrict/enable rotation to certain axes. 0.0f disables rotation around that axis, 1.0f enables it. </summary>
    public Vector3 Axes = new Vector3(1.0f, 0.0f, 1.0f);

    public Vector3 RotationOffset = new Vector3(0.0f, 0.0f, 0.0f);

#if UNITY_EDITOR
    //Only implemented to display "enabled" in the inspector:
    private void Start() { }
#endif

    private void OnWillRenderObject()
    {
        if (!enabled)
            return;

        Vector3 dir = GetDirection();
        dir.Scale(Axes);
        transform.forward = dir;

        if(RotationOffset.x != 0.0f || RotationOffset.y != 0.0f || RotationOffset.z != 0.0f)
            transform.Rotate(RotationOffset, Space.Self);
    }

    private Vector3 GetDirection()
    {
        //if (BillboardMode == Mode.Rotation)
        //    return Camera.current.transform.forward;

        //else if (BillboardMode == Mode.Position)
            return transform.position - Camera.current.transform.position;

        //Debug.Assert(false, "Unknown " + typeof(Mode).Name + ": " + BillboardMode, gameObject);
        //return Vector3.zero;
    }
}
