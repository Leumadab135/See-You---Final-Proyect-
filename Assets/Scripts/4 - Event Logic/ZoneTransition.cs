using System.Collections;
using UnityEngine;

public class ZoneTransition : MonoBehaviour
{

    [SerializeField] private CameraConfinerController _cameraConfiner;
    [SerializeField] private Transform _targetPoint;
    [SerializeField] private Collider2D _newConfiner;
    [SerializeField] private NarrativeEvent _event;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        StartCoroutine(TransitionRoutine(other.transform));
    }

    private IEnumerator TransitionRoutine(Transform player)
    {
        GameStateController.Instance.SetState(GameState.Cinematic);

        yield return FadeController.Instance.FadeOut();

        player.position = _targetPoint.position;

        _cameraConfiner.SetConfiner(_newConfiner);

        if (_event != null)
            _event.Play();

        yield return FadeController.Instance.FadeIn();

        GameStateController.Instance.SetState(GameState.Exploration);
    }
}

