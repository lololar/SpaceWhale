using UnityEngine;
using System.Collections;

public class HookDetection : MonoBehaviour {

    Player _player;

	// Use this for initialization
	void Start () {
        _player = GetComponentInParent<Player>();
	}
	
	// Update is called once per frame
	void Update () {

    }


    void OnTriggerEnter(Collider coll)
    {
        if (coll.CompareTag("Enemy") && _player._animAtt != null)
        {
            _player.Attack(coll.GetComponent<Entity>(), _player._CACDamageRatio);
        }
    }
}
