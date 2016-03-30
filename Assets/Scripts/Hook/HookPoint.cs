using UnityEngine;
using System.Collections;

public class HookPoint : MonoBehaviour {

    Material _possible;
    Material _notPossible;

    Renderer _render;
    
	void Start () {
        _possible = Resources.Load<Material>("Material/HookPossible");
        _notPossible = Resources.Load<Material>("Material/HookNotPossible");

        _render = GetComponent<Renderer>();
    }

    public void Targeted()
    {
        _render.material = _possible;
    }

    public void Untargeted()
    {
        _render.material = _notPossible;
    }
}
