using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PlateformManager : MonoBehaviour {

    public static PlateformManager Instance;

    void Awake()
    {
        if(Instance == null)
            Instance = this;
    }

    public List<GameObject> _meteors;
    public float _refeshTime = 0.1f;
    private bool _hasToRender = true;

    // Use this for initialization
    void Start ()
    {
        _meteors = new List<GameObject>();
        //StartCoroutine(CheckVisible());
	}

    private IEnumerator CheckVisible()
    {
        while(_hasToRender)
        {
            yield return new WaitForSeconds(_refeshTime);
        }
        
    }
}
