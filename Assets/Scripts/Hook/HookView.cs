using UnityEngine;
using System.Collections;

public class HookView : MonoBehaviour {

    void OnTriggerEnter(Collider coll)
    {
        
        if (coll.CompareTag("Plateform") && !PlateformManager.Instance._meteors.Contains(coll.gameObject))
        {
            PlateformManager.Instance._meteors.Add(coll.gameObject);
        }
    }

    void OnTriggerExit(Collider coll)
    {
        if (coll.CompareTag("Plateform") && PlateformManager.Instance._meteors.Contains(coll.gameObject))
        {
            PlateformManager.Instance._meteors.Remove(coll.gameObject);
        }
    }
}
