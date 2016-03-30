using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class Entity : MonoBehaviour
{

    [Header("Entity")]

    public float _life;
    public float _damage;
    public float _delayAttack;

    [Header("Hit")]

    public float _hitTime = 1f;
    public int _flashOccurence = 10;

    protected List<Entity> _targets;

    protected Material _hit;
    protected Material _normal;

    Renderer _render;

    public virtual void Start()
    {
        _targets = new List<Entity>();
        _render = GetComponent<Renderer>();
    }

    public virtual void Attack(Entity other)
    {
        other.Hit(_damage);
    }

    public virtual void Hit(float damage)
    {
        _life -= damage;
        if(_life <= Values.Epsylon)
        {
            StartCoroutine(Dead());
        }
        else
        {
            StartCoroutine(Hit());
        }
    }
    
    public void AddTarget(Entity enemy)
    {
        if(!_targets.Contains(enemy))
        {
            _targets.Add(enemy);
        }
    }

    public void RemoveTarget(Entity enemy)
    {
        if (_targets.Contains(enemy))
        {
            _targets = _targets.Where(entity => enemy != null).ToList();
            _targets.Remove(enemy);
        }
    }

    protected IEnumerator Hit()
    {
        float currentTime = 0;
        bool hitRender = false;

        while(currentTime + Values.Epsylon < _hitTime)
        {
            if(_render && _hit && _normal)
            {
                Debug.Log("IsRender");
                if(!hitRender)
                {
                    _render.material = _normal;
                    hitRender = true;
                }
                else
                {
                    _render.material = _hit;
                }
                hitRender = !hitRender;
            }
            
            currentTime += _hitTime / _flashOccurence;

            yield return new WaitForSeconds(_hitTime / _flashOccurence);
        }

        _render.material = _normal;
    }

    protected IEnumerator Dead()
    {
        yield return 0;
    }
}
