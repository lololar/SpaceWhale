using UnityEngine;
using System.Collections;
using System;

public class LaserTurret : MonoBehaviour
{

    GameObject _leftLaser;
    GameObject _rightLaser;

    GameObject _rightLaserRenderer;
    GameObject _leftLaserRenderer;

    GameObject _startPoint;

    public Vector2 _leftLaserSpeed;
    public Vector2 _rightLaserSpeed;

    public bool _combinedBeams;
    public float _bonusRatioCombination;
    public float _distanceMinBetLaser;

    public float _maxRangeLaser;
    public float _radiusLaser;

    // Use this for initialization
    void Start()
    {
        _leftLaser = ModeManager.Instance._cameraLaserTurret.transform.FindChild("LeftLaser").gameObject;
        _rightLaser = ModeManager.Instance._cameraLaserTurret.transform.FindChild("RightLaser").gameObject;
        _rightLaserRenderer = _rightLaser.transform.GetChild(0).GetChild(0).gameObject;
        _leftLaserRenderer = _leftLaser.transform.GetChild(0).GetChild(0).gameObject;
        _startPoint = ModeManager.Instance._cameraLaserTurret.transform.FindChild("StartLaser").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (ModeManager.Instance._currentMode == ModeManager.Mode.LASERTURRET)
        {

            float h = Input.GetAxis("Horizontal") * _leftLaserSpeed.x * Time.deltaTime;
            float v = Input.GetAxis("Vertical") * _leftLaserSpeed.y * Time.deltaTime;

            float x = Input.GetAxis("Mouse Y") * _rightLaserSpeed.y * Time.deltaTime + _rightLaserSpeed.y * Input.GetAxis("Vertical2") * Time.deltaTime;
            float y = Input.GetAxis("Mouse X") * _rightLaserSpeed.x * Time.deltaTime + _rightLaserSpeed.y * Input.GetAxis("Horizontal2") * Time.deltaTime;

            _leftLaser.transform.Translate(h, v, 0.0f);
            _rightLaser.transform.Translate(y, x, 0.0f);

            bool droit = Input.GetButton("FireRightLaser");
            bool gauche = Input.GetButton("FireLeftLaser");

            if(_combinedBeams)
            {
                if(Vector3.Distance(_rightLaser.transform.position, _leftLaser.transform.position) > _distanceMinBetLaser)
                {
                    _combinedBeams = false;
                }
                else
                {
                    Laser(droit,gauche);
                }
            }
            else
            {
                if(Vector3.Distance(_rightLaser.transform.position, _leftLaser.transform.position) < _distanceMinBetLaser)
                {
                    _combinedBeams = true;
                }
                else
                {
                    Laser(droit, gauche);
                }
            }
        }
    }

    private void Laser(bool droit, bool gauche)
    {
        if(droit)
        {
            Vector3 direct = (_rightLaser.transform.position - _startPoint.transform.position).normalized;
            _rightLaserRenderer.transform.position = _startPoint.transform.position + direct * _maxRangeLaser;
            _rightLaserRenderer.transform.localRotation = Quaternion.LookRotation(direct);
            _rightLaserRenderer.SetActive(true);
        }
        else
        {
            _rightLaserRenderer.SetActive(false);
        }
        if(gauche)
        {
            Vector3 direct = (_leftLaser.transform.position - _startPoint.transform.position).normalized;
            _leftLaserRenderer.transform.position = _startPoint.transform.position + direct * _maxRangeLaser;
            _leftLaserRenderer.transform.localRotation = Quaternion.LookRotation(direct);
            _leftLaserRenderer.SetActive(true);
        }
        else
        {
            _leftLaserRenderer.SetActive(false);
        }
    }
}
