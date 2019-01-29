using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    [SerializeField]
    private Rigidbody _baseRigidbody;
    [SerializeField]
    private Collider _baseCollider;

    [SerializeField]
    private float _moveSpeed = 3.0f;

    private Vector3 _moveDir;
    public bool IsMoving { get { return (_moveDir.x != 0.0f || _moveDir.y != 0.0f) && _movePercent != 0.0f; } }

    private Vector3 _lookDir;

    private float _movePercent = 1.0f;
    public float MovePercent { get { return _movePercent; } set { _movePercent = value; } }

    private void Reset()
    {
        if (!_baseRigidbody)
            _baseRigidbody = GetComponent<Rigidbody>();

        if (!_baseCollider)
            _baseCollider = GetComponent<Collider>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if(_baseRigidbody)
        {
            if(IsMoving)
            {
                _baseRigidbody.position += _moveDir * _moveSpeed * _movePercent * Time.fixedDeltaTime;
            }


        }
    }

    public void SetMoveDir(Vector3 moveDir)
    {
        _moveDir = moveDir;
    }



}
