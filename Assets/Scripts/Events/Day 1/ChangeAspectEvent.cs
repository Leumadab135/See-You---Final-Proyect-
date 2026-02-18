using System.Collections;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class ChangeAspectEvent : NarrativeEvent
{
    [SerializeField] private CinemachineVirtualCameraBase _vCamBase;
    [SerializeField] private GameObject _playerNewDay;
    [SerializeField] private GameObject _playerLastDay;

    protected override IEnumerator EventRoutine()
    {
        _playerLastDay.SetActive(false);
        _playerNewDay.SetActive(true);
        _vCamBase.Follow = _playerNewDay.transform;

        yield return null;
    }
}
