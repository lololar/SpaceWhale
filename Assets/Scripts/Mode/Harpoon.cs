using UnityEngine;
using System.Collections;
using System;

public class Harpoon : MonoBehaviour {

    //GameObject _harpoonPoint;
    GameObject _harpoonStart;
    GameObject _harpoonEnd;

    GameObject _harpoonProjectile;
    public float _timeBeforeLaunch = 3.0f;
    public float _timeByUnit = 0.2f;
    public float _timeBeforeReturn = 3.2f;
    private Coroutine _harpoonLaunch;

    // Use this for initialization
    void Start ()
    {
        //_harpoonPoint = ModeManager.Instance._cameraHarpoon.transform.FindChild("HarpoonPoint").gameObject;
        _harpoonStart = transform.FindChild("StartPoint").gameObject;
        _harpoonEnd = GameObject.Find("HarpoonEnd");
        _harpoonProjectile = _harpoonStart.transform.GetChild(0).gameObject;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(ModeManager.Instance._currentMode == ModeManager.Mode.HARPOON)
        {
            if (Input.GetButtonDown("FireHarpoon") && _harpoonLaunch == null)
            {
                _harpoonLaunch = StartCoroutine(HarpoonLaunch());
            }
        }
    }

    private IEnumerator HarpoonLaunch()
    {

        yield return new WaitForSeconds(_timeBeforeLaunch);

        float timeNeed = Vector3.Distance(_harpoonStart.transform.position, _harpoonEnd.transform.position) * _timeByUnit;
        Vector3 displacementPerSecond = (-_harpoonStart.transform.position + _harpoonEnd.transform.position) / timeNeed;

        float currentTime = 0.0f;

        while ((currentTime += Time.deltaTime) < timeNeed)
        {
            _harpoonStart.transform.GetChild(0).position += displacementPerSecond * Time.deltaTime;
            yield return 0;
        }

        yield return new WaitForSeconds(_timeBeforeReturn);
        
        displacementPerSecond = (_harpoonStart.transform.position - _harpoonEnd.transform.position) / timeNeed;

        currentTime = 0.0f;

        while ((currentTime += Time.deltaTime) < timeNeed)
        {
            _harpoonProjectile.transform.position += displacementPerSecond * Time.deltaTime;
            yield return 0;
        }

    }
}
