using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody _rigibody = null;
    [SerializeField]
    private Camera _camera = null;

    [Header("Movement")]
    [SerializeField]
    private float _moveSpeed = 1.0f;
    [SerializeField]
    private float _walkSpeedFactor = 0.5f;

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
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (GameManager.Instance.IsDialogueRunning)
            return;

        float fixedDelta = Time.fixedDeltaTime;

        Vector2 dir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        Transform camTransform = _camera.transform;

        Vector3 curMoveDir = (camTransform.forward * dir.y + camTransform.right * dir.x);
        curMoveDir.y = 0.0f;

        if (Input.GetKey(KeyCode.LeftShift))
            curMoveDir *= _walkSpeedFactor;

        _rigibody.position += curMoveDir * fixedDelta * _moveSpeed;

        if (GameManager.Instance.MouseLocked)
        {
            float rotationY = Input.GetAxis("Mouse X");
            float rotationX = Input.GetAxis("Mouse Y") * (_invertY ? 1.0f : -1.0f);

            float curLookSpeed = fixedDelta * _lookSpeed;

            _curRotationCam.x += rotationX * curLookSpeed;
            _curRotationCam.x = Mathf.Clamp(_curRotationCam.x, _minLook, _maxLook);
            _curRotationCam.y += rotationY * curLookSpeed;
            camTransform.localRotation = Quaternion.Euler(_curRotationCam);
        }
    }
}
