﻿using UnityEngine;
using System.Collections;

public class MoveCamera : MonoBehaviour
{

    Transform _cameraPivot;
    Camera _cameraScript;

    public float _minXRotation;
    public float _maxXRotation;

    public Vector2 _mouseSensitivity;
    public Vector2 _gamepadSensitivity;
    
    void Start()
    {
        _cameraPivot = transform;
        _cameraScript = transform.FindChild("Pivot").FindChild("Camera").GetComponent<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }
    
    void Update()
    {
        float x = -Input.GetAxis("Mouse Y") * _mouseSensitivity.y + _gamepadSensitivity.y * Input.GetAxis("Vertical2") + _cameraPivot.eulerAngles.x;
        float y = Input.GetAxis("Mouse X") * _mouseSensitivity.x + _gamepadSensitivity.y * Input.GetAxis("Horizontal2") + _cameraPivot.eulerAngles.y;

        _cameraPivot.rotation = Quaternion.Euler(x, y, 0.0f);

        if (_cameraPivot.eulerAngles.x < 360.0f - _minXRotation && _cameraPivot.eulerAngles.x > _maxXRotation)
        {
            if (_cameraPivot.eulerAngles.x < (360.0f - _minXRotation - _maxXRotation) / 2)
            {
                _cameraPivot.eulerAngles = new Vector3(_maxXRotation, _cameraPivot.eulerAngles.y, _cameraPivot.eulerAngles.z);
            }
            else if (_cameraPivot.eulerAngles.x >= (360.0f - _minXRotation - _maxXRotation) / 2)
            {
                _cameraPivot.eulerAngles = new Vector3(360f - _minXRotation, _cameraPivot.eulerAngles.y, _cameraPivot.eulerAngles.z);
            }
        }

        if (Application.isEditor && Input.GetKeyDown(KeyCode.Escape))
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }

    }
}
