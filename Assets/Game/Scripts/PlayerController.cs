﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [System.Serializable]
    public class ItemEvent : UnityEvent<Item> { }

    [SerializeField]
    private Actor _actor = null;

    [SerializeField]
    private Camera _camera = null;
    [SerializeField]
    private Transform _center = null;
    public Transform Center { get { return _center; } }

    [Header("Look")]
    [SerializeField]
    private bool _invertY = false;
    [SerializeField]
    private float _lookSpeed = 1.0f;

    [SerializeField]
    private float _minLook = -85.0f;
    [SerializeField]
    private float _maxLook = 85.0f;

    [Header("Interaction")]
    [SerializeField]
    private float _interactRadius = 1.0f;
    [SerializeField]
    private LayerMask _interactLayerMask = 0;


    private Vector3 _curRotationCam;

    [Header("Damage")]
    [SerializeField]
    private float _health = 100.0f;
    [SerializeField]
    private float _maxHealth = 100.0f;

    [SerializeField]
    private float _dmgClose = 100.0f;

    [SerializeField]
    private float _dmgFar = 10.0f;
    [SerializeField]
    private float _dmgFacingFactor = 2.0f;

    [SerializeField]
    private List<Item> _inventory = null;

    [SerializeField]
    private CamShake _shake = null;
    [SerializeField]
    private float _minFacingDmg = 0.25f;

    [HideInInspector]
    public ItemEvent AddedItemEvent = new ItemEvent();
    [HideInInspector]
    public ItemEvent RemovedItemEvent = new ItemEvent();

   

    public Vector3 GetViewDir()
    {
        return _camera.transform.forward;
    }

    public void ChangeHealth(float delta)
    {
        _health = Mathf.Clamp(_health + delta, 0.0f, _maxHealth);

        if(_health <= 0.0f)
        {
            GameManager.Instance.Respawn();
        }
    }

    public void OnLookAtIrrwish(float distT, float facingT, float opaqueness)
    {
        float totalFactor = facingT * Mathf.Clamp(_dmgFacingFactor, _minFacingDmg, 1.0f) * (1.0f - opaqueness);

        if (_shake)
        {
            _shake.ReduceTrauma = false;
            _shake.AddTrauma(totalFactor * Time.deltaTime);
        }

        float dmg = Mathf.Lerp(_dmgClose, _dmgFar, distT) * totalFactor;
        ChangeHealth(dmg * Time.deltaTime);
    }

    private void Update()
    {
        if (GameManager.Instance.IsDialogueRunning)
        {
            GameManager.Instance.SetInteractPreviewString(null);
            return;
        }

        Transform camTransform = _camera.transform;

        Ray ray = new Ray(camTransform.position, _camera.transform.forward);
        string previewString = "";

        if (Physics.Raycast(ray, out RaycastHit hit, _interactRadius, _interactLayerMask))
        {
            var interactable = hit.collider.GetComponent<AInteractable>();

            if (interactable)
            {
                previewString = interactable.GetPreviewString();

                if (Input.GetMouseButtonDown(0))
                {
                    interactable.Activate(this);
                }
            }
        }

        GameManager.Instance.SetInteractPreviewString(previewString);

        if (GameManager.Instance.MouseLocked)
        {
            float rotationY = Input.GetAxis("Mouse X");
            float rotationX = Input.GetAxis("Mouse Y") * (_invertY ? 1.0f : -1.0f);

            float curLookSpeed = Time.deltaTime * _lookSpeed;

            _curRotationCam.x += rotationX * curLookSpeed;
            _curRotationCam.x = Mathf.Clamp(_curRotationCam.x, _minLook, _maxLook);
            _curRotationCam.y += rotationY * curLookSpeed;
            camTransform.localRotation = Quaternion.Euler(_curRotationCam);
        }

        Vector2 dir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        Vector3 curMoveDir = (camTransform.forward * dir.y + camTransform.right * dir.x);
        curMoveDir.y = 0.0f;

        if (curMoveDir.sqrMagnitude > 1.0f)
            curMoveDir.Normalize();

        _actor.SetMoveDir(curMoveDir);
    }

    public void GiveItem(Item item)
    {
        _inventory.Add(item);
        AddedItemEvent.Invoke(item);
    }

    public bool HasItem(string itemName)
    {   
        return HasItem(GameManager.Instance.GetItem(itemName));
    }

    public bool HasItem(Item item)
    {
        return _inventory.Contains(item);
    }

    public void RemoveItem(Item item)
    {
        _inventory.Remove(item);
        RemovedItemEvent.Invoke(item);
    }

    public void Respawn(Checkpoint currentCheckpoint)
    {
        transform.position = currentCheckpoint.GetSpawnPos();
        _health = _maxHealth;
    }
}
