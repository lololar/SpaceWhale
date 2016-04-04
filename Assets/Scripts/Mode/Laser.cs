using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour {

    LaserTurret _turret;
    public float _damage;

	// Use this for initialization
	void Start () {
        _turret = GameObject.FindGameObjectWithTag("LaserTurret").GetComponent<LaserTurret>();
	}
	
    public void OnTriggerEnter(Collider coll)
    {
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
