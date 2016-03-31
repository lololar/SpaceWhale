using UnityEngine;
using System.Collections;
using System;

public class HookLaser : MonoBehaviour {
    
    public float _range;

    GameObject _hook;
    GameObject _enemy;

    Player _player;

    Coroutine _laser;

    public float _reloadTime;
    public float _laserDamageRatio;

    // Use this for initialization
    void Start () {
        _player = GetComponent<Player>();
        _hook = GameObject.FindGameObjectWithTag("Hook");
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (_laser == null)
        {
            Ray ray = new Ray(_hook.transform.position, _hook.transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, _range) && hit.collider.gameObject && hit.collider.gameObject.CompareTag("Enemy"))
            {
                if (!_enemy)
                {
                    _enemy = hit.collider.gameObject;
                    //_hookPoint.GetComponentInChildren<HookPoint>().Targeted();
                }
                else
                {
                    Debug.Log("Has Enemy");
                }
            }
            else if (_enemy)
            {
                //_hookPoint.GetComponentInChildren<HookPoint>().Untargeted();
                _enemy = null;
            }

            if (Input.GetButtonDown("Laser") && _enemy)
            {
                Debug.Log("Laser");
                _player.Attack(_enemy.GetComponent<Entity>(), _laserDamageRatio);
                _laser = StartCoroutine(Laser());
            }
        }
    }

    private IEnumerator Laser()
    {
        yield return new WaitForSeconds(_reloadTime);
        Debug.Log("Reloaded");
        _laser = null;
    }
}
