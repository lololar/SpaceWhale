using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {

    Rigidbody _myRigid;

    public float _speed;
    
	void Start () {
        _myRigid = GetComponent<Rigidbody>();
	}
	
	void FixedUpdate () {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if(Mathf.Abs(h) > 0.3f)
        {
            _myRigid.AddForce(_speed * h * Vector3.right);
        }
        if (Mathf.Abs(v) > 0.3f)
        {
            _myRigid.AddForce(_speed * v * Vector3.forward);
        }
    }
}
