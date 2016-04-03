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
    private GameObject _targetHookable;
    private Vector3 _targetPoint;
    private GameObject _currentHookable;

    // Use this for initialization
    void Start()
    {
        _hook = GameObject.FindGameObjectWithTag("Hook");
        _currentHookable = GameObject.Find("ShipPlat");
        EndHookshot();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (ModeManager.Instance._currentMode == ModeManager.Mode.PLAYER && _hookshot == null)
        {
            List<GameObject> meteors = PlateformManager.Instance._meteors;
            GameObject nearMeteor = null;


            for (int i = 0; i < meteors.Count; i++)
            {
                GameObject meteor = meteors[i];
                meteor.GetComponent<Renderer>().material.color = Color.green;
                if (meteor != _currentHookable)
                {
                    if (nearMeteor)
                    {
                        if (Vector3.Distance(transform.position, nearMeteor.transform.position) > Vector3.Distance(transform.position, meteor.transform.position))
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
            if (nearMeteor)
            {
                nearMeteor.GetComponent<Renderer>().material.color = Color.blue;
                if (Input.GetButtonDown("Hook"))
                {
                    _targetHookable = nearMeteor;

                    Vector3 direction = _targetHookable.transform.position - transform.position;

                    _targetPoint = Vector3.Scale((Vector3.right * direction.x + Vector3.forward * direction.z).normalized, _targetHookable.transform.lossyScale / 2) + _targetHookable.transform.position;

                    /*Ray ray = new Ray(transform.position, _targetPoint);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit) && hit.collider.gameObject)
                    {
                        Debug.Log(transform.position + "            " + _targetPoint + "             " + nearMeteor.transform.position);
                    }*/

                    _hookshot = StartCoroutine(Hookshot());
                }
            }
        }

    }

    public IEnumerator Hookshot()
    {
        Vector3 posDepart = transform.position;
        Vector3 posArrivee = _targetPoint;
        float timeNeed = (Vector3.Distance(posDepart, posArrivee) / _range) * _timeAtMaxRange;
        Vector3 displacementPerSecond = (-posDepart + posArrivee) / timeNeed;
        float currentTime = 0.0f;

        while ((currentTime += Time.deltaTime) < timeNeed)
        {
            if (GetComponent<Rigidbody>())
            {
                GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
            transform.position += displacementPerSecond * Time.deltaTime;
            yield return 0;
        }

    }

    void EndHookshot()
    {
        if (_hookshot != null)
        {
            StopCoroutine(_hookshot);
            _hookshot = null;
        }
        transform.position = _currentHookable.transform.FindChild("Endhook").position;
    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.CompareTag("Plateform") && coll.gameObject != _currentHookable)
        {
            _currentHookable = coll.gameObject;
            if (_hookshot != null)
            {
                EndHookshot();
            }
        }
    }

    void OnCollisionExit(Collision coll)
    {
        if (coll.gameObject.CompareTag("Plateform") && coll.gameObject == _currentHookable)
        {
            Ray ray = new Ray(transform.position + Vector3.down, Vector3.down);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject && hit.collider.gameObject.CompareTag("Plateform"))
            {
                _currentHookable = hit.collider.gameObject;
            }
            else
            {
                _currentHookable = null;
            }
        }
    }
}
