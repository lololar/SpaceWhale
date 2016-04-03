﻿using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour {

    LaserTurret _turret;
    public float _damage;

	// Use this for initialization
	void Start () {
        _turret = GameObject.FindGameObjectWithTag("LaserTurret").GetComponent<LaserTurret>();
	}
	
    void OnTriggerEnter(Collider coll)
    {
        Debug.Log(coll.tag);
        if(coll.CompareTag("Enemy"))
        {
            Entity ent = coll.GetComponent<Enemy>();
            if(_turret._combinedBeams)
            {
                ent.Hit(_damage * _turret._bonusRatioCombination);
            }
            else
            {
                ent.Hit(_damage);
            }
        }
    }
}
