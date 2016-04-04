using UnityEngine;
using System.Collections;
using System;

public class Player : Entity {

    Coroutine _animAtt;

    Transform _ship;
    GameObject _hook;

    public float _CACDamageRatio;

    public override void Start()
    {
        base.Start();
        _ship = GameObject.FindGameObjectWithTag("Ship").transform.GetChild(0);
        _hook = GameObject.FindGameObjectWithTag("Hook").transform.parent.gameObject;
        _hit = Resources.Load<Material>("Material/PlayerHit");
        _normal = Resources.Load<Material>("Material/Player");
    }


    void Update () {
	    if(ModeManager.Instance._currentMode == ModeManager.Mode.PLAYER && Input.GetButtonDown("CAC") && _animAtt == null)
        {
            _animAtt = StartCoroutine(AttackAnim());
        }
	}

    IEnumerator AttackAnim()
    {
        float currentTime = 0;

        float animationFrame = 50;

        while (currentTime + Values.Epsylon < _delayAttack)
        {
            /*float x = -Mathf.Sin(currentTime * Values.PI / _delayAttack * 2.0f) * 45.0f;

            _hook.transform.localRotation = Quaternion.Euler(x, _hook.transform.rotation.y, _hook.transform.rotation.z);*/

            float y = -Mathf.Sin(currentTime * Values.PI / _delayAttack * 2.0f) * 45.0f;

            _hook.transform.localRotation = Quaternion.Euler(_hook.transform.rotation.x, y, _hook.transform.rotation.z);

            currentTime += _delayAttack / animationFrame;
            
            yield return new WaitForSeconds(_delayAttack / animationFrame);
        }
        _animAtt = null;
    }

    public void Respawn()
    {
        transform.position = _ship.FindChild("Endhook").position;
    }
}
