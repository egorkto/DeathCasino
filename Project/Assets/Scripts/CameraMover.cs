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
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnDestroy()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void Update()
    {
        _xRotation += Input.GetAxis("Mouse Y") * _sensitivity;
        _yRotation += Input.GetAxis("Mouse X") * _sensitivity;
        _xRotation = Mathf.Clamp(_xRotation, _xRotationRange.x, _xRotationRange.y);
        _yRotation = Mathf.Clamp(_yRotation, _yRotationRange.x, _yRotationRange.y);
        _camera.transform.localRotation = Quaternion.Euler(-_xRotation, _yRotation, 0);
    }
}
