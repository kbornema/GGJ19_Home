using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateLight : MonoBehaviour
{
    [SerializeField]
    private Light _light = null;
    [SerializeField]
    private AnimationCurve _intensity = null;
    [SerializeField]
    private float _min = 4.0f;
    [SerializeField]
    private float _max = 6.0f;

    [SerializeField]
    private float _time = 2.0f;

    private float _curTime;

    private void Awake()
    {
        _curTime = UnityEngine.Random.value * _time;
    }

    // Update is called once per frame
    private void Update()
    {
        _curTime += Time.deltaTime;

        float t = _curTime / _time;
        _light.intensity = Mathf.Lerp(_min, _max, _intensity.Evaluate(t));
    }
}
