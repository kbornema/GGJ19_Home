using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBhvr : MonoBehaviour
{
    [SerializeField, Range(0.0f, 1.0f)]
    private float _opaque = 1.0f;
    public float Opaque { get { return _opaque; } }
}
