using System.Collections;
using UnityEngine;
using Unity.Cinemachine;

public class CameraZoomEvent : NarrativeEvent
{
    [SerializeField] private CinemachineCamera _camera;
    [SerializeField] private CinemachineConfiner2D _confiner;

    [SerializeField] private float _targetSize = 8f;
    [SerializeField] private float _blendDuration = 1f;
    [SerializeField] private float _holdTime = 2f;
    [SerializeField] private bool _restoreOriginal = true;

    protected override IEnumerator EventRoutine()
    {
        GameStateController.Instance.SetState(GameState.Cinematic);

        var lens = _camera.Lens;
        float startSize = lens.OrthographicSize;

        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime / _blendDuration;
            lens.OrthographicSize = Mathf.Lerp(startSize, _targetSize, t);
            _camera.Lens = lens;

            _confiner.InvalidateBoundingShapeCache();

            yield return null;
        }

        if (_holdTime > 0)
            yield return new WaitForSeconds(_holdTime);

        if (_restoreOriginal)
        {
            t = 0f;

            while (t < 1f)
            {
                t += Time.deltaTime / _blendDuration;
                lens.OrthographicSize = Mathf.Lerp(_targetSize, startSize, t);
                _camera.Lens = lens;

                _confiner.InvalidateBoundingShapeCache();

                yield return null;
            }
        }

        GameStateController.Instance.SetState(GameState.Exploration);
    }
}
