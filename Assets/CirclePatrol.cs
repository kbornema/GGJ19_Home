using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CirclePatrol : MonoBehaviour
{
    [SerializeField]
    private float _radius = 5.0f;

    [SerializeField]
    private float _time = 10.0f;
    [SerializeField]
    private Transform _center = null;

    [SerializeField]
    private Transform _root;
    private float _curTime = 0.0f;

    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _curTime += Time.deltaTime;

        float t = _curTime / _time;

        Vector3 curPos = _center.position + new Vector3(Mathf.Sin(t) * _radius, 0.0f, Mathf.Cos(t) * _radius);
        curPos.y = 0.0f;

        _root.position = curPos;
    }
}
