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

    // Use this for initialization
    void Start ()
    {
        _meteors = new List<GameObject>();
	}
}
