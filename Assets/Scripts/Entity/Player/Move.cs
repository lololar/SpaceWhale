using UnityEngine;
using System.Collections;
using System;

public class Move : MonoBehaviour
{
    Rigidbody _myRigid;
    Player _player;
    Vector3 direction = Vector3.right + Vector3.forward;

    public float _speed;
    public bool _isAttached;

    [HideInInspector]
    public GameObject _surfing;

    void Start()
    {
        _myRigid = GetComponent<Rigidbody>();
        _player = GetComponent<Player>();
    }

    void FixedUpdate()
    {
        if (Input.GetButtonDown("ChangeMode"))
        {
            Ray ray = new Ray(transform.position, Vector3.down);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.CompareTag("HarpoonShooter"))
                {
                    if (ModeManager.Instance._currentMode == ModeManager.Mode.PLAYER)
                    {
                        ModeManager.Instance.ChangeMode(ModeManager.Mode.HARPOON);
                    }
                    else
                    {
                        ModeManager.Instance.ChangeMode(ModeManager.Mode.PLAYER);
                    }
                }
                if (hit.collider.gameObject.CompareTag("LaserTurret"))
                {
                    if (ModeManager.Instance._currentMode == ModeManager.Mode.PLAYER)
                    {
                        ModeManager.Instance.ChangeMode(ModeManager.Mode.LASERTURRET);
                    }
                    else if (ModeManager.Instance._currentMode == ModeManager.Mode.LASERTURRET)
                    {
                        ModeManager.Instance.ChangeMode(ModeManager.Mode.PLAYER);
                    }
                }
            }

        }
        if (ModeManager.Instance._currentMode == ModeManager.Mode.PLAYER)
        {
            if (_isAttached)
            {
                if (Input.GetButtonDown("HarpoonBoard"))
                {
                    ToggleAttach();
                }
                else
                {
                    transform.position = _surfing.transform.position;
                }
            }
            else
            {
                if (Input.GetButtonDown("HarpoonBoard"))
                {
                    Ray ray = new Ray(transform.position, Vector3.down);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit) && hit.collider.gameObject)
                    {
                        if (hit.collider.CompareTag("Harpoon"))
                        {
                            ToggleAttach(hit.transform.GetChild(0).gameObject);
                        }
                    }
                }
                else
                {
                    float h = Input.GetAxis("Horizontal");
                    float v = Input.GetAxis("Vertical");

                    if (Mathf.Abs(h) > 0.3f)
                    {
                        _myRigid.AddForce(_speed * h * Vector3.Scale(transform.right, direction));
                    }
                    if (Mathf.Abs(v) > 0.3f)
                    {
                        _myRigid.AddForce(_speed * v * Vector3.Scale(transform.forward, direction));
                    }
                }
            }
        }
    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject && coll.gameObject.CompareTag("DeathZone"))
        {
            _player.Respawn();
        }
    }

    public void ToggleAttach(GameObject obj = null)
    {
        _isAttached = !_isAttached;
        _surfing = obj;
    }
}
