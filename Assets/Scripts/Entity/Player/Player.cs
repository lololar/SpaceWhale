using UnityEngine;
using System.Collections;
using System;

public class Player : Entity {

    Coroutine animAtt;

    public override void Start()
    {
        base.Start();
        _hit = Resources.Load<Material>("Material/PlayerHit");
        _normal = Resources.Load<Material>("Material/Player");
    }


    void Update () {
	    if(Input.GetMouseButtonDown(0) && animAtt == null)
        {
            animAtt = StartCoroutine(AttackAnim());
        }
	}

    IEnumerator AttackAnim()
    {
        for (int i = 0; i < _targets.Count; i++)
        {
            Attack(_targets[i]);
        }
        yield return new WaitForSeconds(_delayAttack);
        animAtt = null;
    }
}
