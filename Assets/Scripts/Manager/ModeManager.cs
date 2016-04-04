using UnityEngine;
using System.Collections;
using System;

public class ModeManager : MonoBehaviour {

    public enum Mode
    {
        PLAYER,
        LASERTURRET,
        HARPOON,
    }

    public static ModeManager Instance;

    public Mode _currentMode;

    [HideInInspector]
    public GameObject _cameraPlayer;
    [HideInInspector]
    public GameObject _cameraLaserTurret;
    [HideInInspector]
    public GameObject _cameraHarpoon;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    // Use this for initialization
    void Start () {

        _cameraPlayer = GameObject.Find("CameraPlayer");
        _cameraLaserTurret = GameObject.Find("CameraLaserTurret");
        _cameraHarpoon = GameObject.Find("CameraHarpoon");

        ChangeMode(Mode.PLAYER);
	}

    public void ChangeMode(Mode newMode)
    {
        _currentMode = newMode;
        ActivateCamera();
    }

    private void ActivateCamera()
    {
        switch (_currentMode)
        {
            case Mode.PLAYER:
                _cameraPlayer.SetActive(true);
                _cameraHarpoon.SetActive(false);
                _cameraLaserTurret.SetActive(false);
                break;
            case Mode.LASERTURRET:
                _cameraPlayer.SetActive(false);
                _cameraLaserTurret.SetActive(true);
                _cameraHarpoon.SetActive(false);
                break;
            case Mode.HARPOON:
                _cameraPlayer.SetActive(false);
                _cameraLaserTurret.SetActive(false);
                _cameraHarpoon.SetActive(true);
                break;
            default:
                break;
        }
    }
}
