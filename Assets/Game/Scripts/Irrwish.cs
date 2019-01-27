using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Irrwish : MonoBehaviour
{
    [SerializeField]
    private Light _light = null;
    [SerializeField]
    private SpriteRenderer _outerHalor = null;
    [SerializeField]
    private Transform _center = null;

    [Header("Base")]
    [SerializeField]
    private Gradient _mainColorGradient = null;
    [SerializeField]
    private AnimationCurve _colorCurve = null;
    [SerializeField]
    private AnimationCurve _defaultIntensity = null;
    [SerializeField]
    private float _minDefaultIntensity = 1.0f;
    [SerializeField]
    private float _maxDefaultIntensity = 4.0f;
    [SerializeField]
    private float _timeForCurve = 1.0f;

    [SerializeField]
    private float _upDownTime = 1.0f;
    [SerializeField]
    private AnimationCurve _upDownCurve = null;
    [SerializeField]
    private float _upDownScale = 1.0f;
    private float _curUpDownTime;

    [Header("Aggro")]
    [SerializeField]
    private Gradient _aggroColor = null;
    [SerializeField]
    private float _aggroTimeScale = 2.0f;
    [SerializeField]
    private bool _seesPlayer = false;
    [SerializeField]
    private AnimationCurve _aggroIntensity = null;
    [SerializeField]
    private float _minAggroIntensity = 1.0f;
    [SerializeField]
    private float _maxAggroIntensity = 4.0f;

    [Header("Detection")]
    [SerializeField]
    private LayerMask _rayCastLayerMask = 0;
    [SerializeField]
    private float _aggroRange = 12.0f;
    private float _curColorTime = 0.0f;

    public bool SeesPlayer { get { return _seesPlayer; } }

    private void Start()
    {
        _curColorTime = UnityEngine.Random.value * _timeForCurve;
    }

    // Update is called once per frame
    private void Update()
    {   
       
        CheckPlayerVisible();
    }

    private void CheckPlayerVisible()
    {
        _curColorTime += Time.deltaTime;

        _seesPlayer = false;

        PlayerController player = GameManager.Instance.Player;

        var toPlayer = player.Center.position - _center.position;
        var dist = Vector3.Magnitude(toPlayer);
        toPlayer.Normalize();

        float opaqueness = 0.0f;

        //if player is close:
        if (dist < _aggroRange)
        {
            Ray r = new Ray(_center.position, toPlayer);
            var hits = Physics.RaycastAll(r, _aggroRange, _rayCastLayerMask);

            List<RaycastHit> rayCastHits = new List<RaycastHit>(hits);
            rayCastHits.Sort((x, y) => x.distance.CompareTo(y.distance));

            for (int i = 0; i < rayCastHits.Count; i++)
            {
                RaycastHit hit = rayCastHits[i];
                var hitCollider = hit.collider;

                if (hitCollider.gameObject == player.gameObject)
                {
                    _seesPlayer = true;
                    break;
                }
                
                //if an obstacle is hit:
                else if ((GameManager.Instance.ObstacleLayerMask & (1 << hitCollider.gameObject.layer)) != 0)
                {   
                    var obstacle = hitCollider.GetComponent<ObstacleBhvr>();

                    if (obstacle)
                        opaqueness += obstacle.Opaque;
                    else
                        opaqueness = 1.0f;

                    if (opaqueness >= 1.0f)
                        break;
                }
            }
        }

        opaqueness = Mathf.Clamp01(opaqueness);

        if (_seesPlayer && opaqueness < 1.0f)
        {
            float t = (_curColorTime * _aggroTimeScale) / _timeForCurve;
            _light.intensity = Mathf.Lerp(_minAggroIntensity, _maxAggroIntensity, _aggroIntensity.Evaluate(t));
            SetCurColor(_aggroColor.Evaluate(_colorCurve.Evaluate(t)));
            
            var playerLookDir = player.GetViewDir();
            //1 if facing player, 0 if not facing player:
            float inFrontT = (Vector3.Dot(playerLookDir, -toPlayer) + 1.0f) * 0.5f;
            player.OnLookAtIrrwish(Mathf.Clamp01(dist / _aggroRange), inFrontT, opaqueness);
        }

        else
        {
            float t = (_curColorTime) / _timeForCurve;
            _light.intensity = Mathf.Lerp(_minDefaultIntensity, _maxDefaultIntensity, _defaultIntensity.Evaluate(t));
            SetCurColor(_mainColorGradient.Evaluate(_colorCurve.Evaluate(t)));
        }

        _curUpDownTime += Time.deltaTime;

        float upDownT = (_curUpDownTime / _upDownTime);

        if(_seesPlayer)
            upDownT *= 2.0f;

        Vector3 pos = transform.position;
        pos.y = _upDownCurve.Evaluate(upDownT) * _upDownScale;
        transform.position = pos;
    }

    private void SetCurColor(Color color)
    {
        _light.color = color;
        _outerHalor.color = color;
    }
}
