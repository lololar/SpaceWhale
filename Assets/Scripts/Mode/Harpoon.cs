using UnityEngine;
using System.Collections;

public class Harpoon : MonoBehaviour {

    GameObject _harpoonPoint;

    // Use this for initialization
    void Start ()
    {
        _harpoonPoint = ModeManager.Instance._cameraHarpoon.transform.FindChild("HarpoonPoint").gameObject;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(ModeManager.Instance._currentMode == ModeManager.Mode.HARPOON)
        {

        }
    }
}
