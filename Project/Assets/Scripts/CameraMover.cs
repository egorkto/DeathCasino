using System;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private float _sensitivity;
    [SerializeField] private Vector2 _xRotationRange;
    [SerializeField] private Vector2 _yRotationRange;

    private float _xRotation, _yRotation;

    private void Start()
    {
        LockCursor();
    }

    private void OnDestroy()
    {
        UnlockCursor();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
            UnlockCursor();
        if (Input.GetMouseButtonDown(0))
            LockCursor();
        _xRotation += Input.GetAxis("Mouse Y") * _sensitivity;
        _yRotation += Input.GetAxis("Mouse X") * _sensitivity;
        _xRotation = Mathf.Clamp(_xRotation, _xRotationRange.x, _xRotationRange.y);
        _yRotation = Mathf.Clamp(_yRotation, _yRotationRange.x, _yRotationRange.y);
        _camera.transform.localRotation = Quaternion.Euler(-_xRotation, _yRotation, 0);
    }

    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
