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

    [SerializeField]
    private Irrwish _irrwish;

    // Update is called once per frame
    private void LateUpdate()
    {
        if (_irrwish && _irrwish.SeesPlayer)
            return;

        _curTime += Time.deltaTime;

        float t = _curTime / _time;

        Vector3 curPos = _center.position + new Vector3(Mathf.Sin(t) * _radius, 0.0f, Mathf.Cos(t) * _radius);
        curPos.y = _root.position.y;

        _root.position = curPos;
    }
}
