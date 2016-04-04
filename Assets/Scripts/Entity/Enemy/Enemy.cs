using UnityEngine;
using System.Collections;
using System;

public class Enemy : Entity {

    public override void Start()
    {
        base.Start();
        AddTarget(GameObject.FindGameObjectWithTag("Player").GetComponent<Entity>());
        _hit = Resources.Load<Material>("Material/EnemyHit");
        _normal = Resources.Load<Material>("Material/Enemy");
    }

    void Update () {
	
	}
	
	protected override void Dead()
	{
		base.Dead();
		Destroy(gameObject);
	}
}
