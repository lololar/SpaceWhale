using UnityEngine;
using System.Collections;
using System;

public class HookShot : MonoBehaviour
{
    public float _range;

    GameObject _hook;
    GameObject _hookPoint;
    public float _minDistanceUpPlateforme;
    public float _timeAtMaxRange;

    Coroutine _hookshot;

    // Use this for initialization
    void Start()
    {
        _hook = GameObject.FindGameObjectWithTag("Hook");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(_hookshot == null)
        {
            Ray ray = new Ray(_hook.transform.position, _hook.transform.forward);
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
        
            if (Input.GetMouseButtonDown(1) && _hookPoint)
            {
                Debug.Log("HookShot");
                _hookshot = StartCoroutine(Hookshot());
            }
        }
        
    }

    public IEnumerator Hookshot()
    {
        Vector3 posDepart = transform.position;
        Vector3 posArrivee = _hookPoint.transform.position;
        float timeNeed = (Vector3.Distance(posDepart, posArrivee) / _range) * _timeAtMaxRange;
        Vector3 displacementPerSecond = (- posDepart + posArrivee) / timeNeed;
        float currentTime = 0.0f;

        if(GetComponent<Rigidbody>())
        {
            GetComponent<Rigidbody>().isKinematic = true;
        }

        while ((currentTime += Time.deltaTime) < timeNeed)
        {
            transform.position += displacementPerSecond * Time.deltaTime;
            yield return 0;
        }

        EndHookshot();

        if (GetComponent<Rigidbody>())
        {
            GetComponent<Rigidbody>().isKinematic = false;
        }

        _hookshot = null;
    }

    void EndHookshot()
    {
        transform.position = _hookPoint.transform.FindChild("Endhook").position;
    }
}
