using UnityEngine;

public class GameTechnicalManager : MonoBehaviour
{
    [Header("Frame Rate")]
    [SerializeField] private int _targetFPS = 60;

    [Header("Resolution")]
    [SerializeField] private int _width = 1920;
    [SerializeField] private int _height = 1080;
    [SerializeField] private bool _fullscreen = true;

    private void Awake()
    {
        ApplyFrameRate();
        ApplyResolution();
    }

    private void ApplyFrameRate()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = _targetFPS;
    }

    private void ApplyResolution()
    {
        Screen.SetResolution(_width, _height, _fullscreen);
    }
}
