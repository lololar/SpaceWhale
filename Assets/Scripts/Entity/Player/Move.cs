using UnityEngine;
using System.Collections;
using System;

public class Move : MonoBehaviour {

    Rigidbody _myRigid;
    Player _player;
    Vector3 direction = Vector3.right + Vector3.forward;

    public float _speed;
    
	void Start () {
        _myRigid = GetComponent<Rigidbody>();
        _player = GetComponent<Player>();
	}
	
	void FixedUpdate () {
        if(ModeManager.Instance._currentMode == ModeManager.Mode.PLAYER)
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            if(Mathf.Abs(h) > 0.3f)
            {
                _myRigid.AddForce(_speed * h * Vector3.Scale(transform.right, direction));
            }
            if (Mathf.Abs(v) > 0.3f)
            {
                _myRigid.AddForce(_speed * v * Vector3.Scale(transform.forward, direction));
            }
        }

    }

    void OnTriggerEnter(Collider coll)
    {
        if(coll.gameObject && coll.CompareTag("DeathZone"))
        {
            _player.Respawn();
        }
    }
}
