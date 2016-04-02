using UnityEngine;
using System.Collections;

public class ModeManager : MonoBehaviour {

    public enum Mode
    {
        PLAYER,
        LASERTURRET,
        HARPOON,
    }

    public static ModeManager Instance;

    public Mode _currentMode;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    // Use this for initialization
    void Start () {
        _currentMode = Mode.PLAYER;
	}

    public void ChangeMode(Mode newMode)
    {
        _currentMode = newMode;
    }
}
