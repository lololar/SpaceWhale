using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class EnemyMove : MonoBehaviour {

    public List<GameObject> _meteorsInRange;
    Rigidbody _rigid;
    Entity _enemy;
    

    public float _maxRangeRepulsion = 1.5f;
    public float _speed = 5;
    public float _repulsionSpeed = 2;
    public float _maxSpeed = 50;

    // Use this for initialization
    void Start () {
        _meteorsInRange = new List<GameObject>();
        _enemy = GetComponent<Enemy>();
        _rigid = GetComponent<Rigidbody>();
	}

    // Update is called once per frame
    void Update () {
            if (_rigid.velocity.magnitude < _maxSpeed)
            {
                _rigid.AddForce((_enemy.GetTarget(0).transform.position - transform.position).normalized * _speed);
            }

            Vector3 force = Vector3.zero;

            for (int i = 0; i < _meteorsInRange.Count; i++)
            {
                GameObject meteor = _meteorsInRange[i];
                force += (meteor.transform.position - transform.position).normalized * (_maxRangeRepulsion - Vector3.Distance(meteor.transform.position, transform.position));
            }
            _rigid.AddForce(force.normalized * _repulsionSpeed);
	}

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.CompareTag("EnemyDodge") && !_meteorsInRange.Contains(coll.gameObject))
        {
            _meteorsInRange.Add(coll.gameObject);
        }
    }

    void OnTriggerExit(Collider coll)
    {
        if (coll.gameObject.CompareTag("EnemyDodge") && _meteorsInRange.Contains(coll.gameObject))
        {
            _meteorsInRange.Remove(coll.gameObject);
        }
    }
}
