using UnityEngine;
using System.Collections;

public class MoveCamera : MonoBehaviour
{

    Transform _cameraPivot;

    public float _minXRotation;
    public float _maxXRotation;

    public Vector2 _mouseSensitivity;
    public Vector2 _gamepadSensitivity;
    
    void Start()
    {
        _cameraPivot = transform;
        Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }
    
    void Update()
    {
        if (ModeManager.Instance._currentMode == ModeManager.Mode.PLAYER)
        {
            float x = -Input.GetAxis("Mouse Y") * _mouseSensitivity.y * Time.deltaTime + _gamepadSensitivity.y * Input.GetAxis("Vertical2") * Time.deltaTime + _cameraPivot.eulerAngles.x;
            float y = Input.GetAxis("Mouse X") * _mouseSensitivity.x * Time.deltaTime + _gamepadSensitivity.y * Input.GetAxis("Horizontal2") * Time.deltaTime + _cameraPivot.eulerAngles.y;

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
        }

#if UnityEditor

        if (Application.isEditor && Input.GetKeyDown(KeyCode.Escape))
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }

#endif

    }
}
