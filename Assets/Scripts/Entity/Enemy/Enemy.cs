using UnityEngine;
using System.Collections;

public class Enemy : Entity {

    public override void Start()
    {
        base.Start();
        _hit = Resources.Load<Material>("Material/EnemyHit");
        _normal = Resources.Load<Material>("Material/Enemy");
    }

    void Update () {
	
	}
}
