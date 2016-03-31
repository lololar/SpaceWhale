using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class HookShot : MonoBehaviour
{
    public float _range;

    GameObject _hook;
    public float _minDistanceUpPlateforme;
    public float _timeAtMaxRange;

    Coroutine _hookshot;
    private GameObject _targetMeteor;
    private GameObject _currentMeteor;

    // Use this for initialization
    void Start()
    {
        _hook = GameObject.FindGameObjectWithTag("Hook");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_hookshot == null)
        {
            /*Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            Debug.DrawRay(ray.origin + Camera.main.transform.forward * 10, ray.direction * _range, Color.blue, 0.2f);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, _range) && hit.collider.gameObject && hit.collider.gameObject.CompareTag("HookZone"))
            {
                if (!_hookPoint)
                {
                    _hookPoint = hit.collider.gameObject;
                    _hookPoint.GetComponentInChildren<HookPoint>().Targeted();
                }
            }
            else if(_hookPoint)
            {
                _hookPoint.GetComponentInChildren<HookPoint>().Untargeted();
                _hookPoint = null;
            }
        
            if (Input.GetButtonDown("Hook") && _hookPoint)
            {
                Debug.Log("HookShot");
                _hookshot = StartCoroutine(Hookshot());
            }*/

            List<GameObject> meteors = PlateformManager.Instance._meteors;
            GameObject nearMeteor = null;


            for (int i = 0; i < meteors.Count; i++)
            {
                GameObject meteor = meteors[i];
                if(meteor != _currentMeteor)
                {
                    if(nearMeteor)
                    {
                        if(Vector3.Distance(transform.position, nearMeteor.transform.position) > Vector3.Distance(transform.position, meteor.transform.position))
                        {
                            nearMeteor = meteor;
                        }
                    }
                    else
                    {
                        nearMeteor = meteor;
                    }
                }
            }
            if(nearMeteor && Input.GetButtonDown("Hook"))
            {
                _targetMeteor = nearMeteor;
                _hookshot = StartCoroutine(Hookshot());
                //transform.position = nearMeteor.transform.position + Vector3.up * 2.0f;
            }
        }
        
    }

    public IEnumerator Hookshot()
    {
        Vector3 posDepart = transform.position;
        Vector3 posArrivee = _targetMeteor.transform.position;
        float timeNeed = (Vector3.Distance(posDepart, posArrivee) / _range) * _timeAtMaxRange;
        Vector3 displacementPerSecond = (- posDepart + posArrivee) / timeNeed;
        float currentTime = 0.0f;

        while ((currentTime += Time.deltaTime) < timeNeed)
            {
            if(GetComponent<Rigidbody>())
            {
                GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
            transform.position += displacementPerSecond * Time.deltaTime;
            Debug.Log("LOL");
            yield return 0;
        }


    }

    void EndHookshot()
    {
        if(_hookshot != null)
        {
            StopCoroutine(_hookshot);
            _hookshot = null;
        }
        transform.position = _currentMeteor.transform.FindChild("Endhook").position;
        Debug.Log("EndHook");
    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.CompareTag("Plateform") && coll.gameObject != _currentMeteor)
        {
            _currentMeteor = coll.gameObject;
            EndHookshot();
        }
        else if(!coll.gameObject.CompareTag("Plateform"))
        {
            _currentMeteor = null;
        }
    }
}
