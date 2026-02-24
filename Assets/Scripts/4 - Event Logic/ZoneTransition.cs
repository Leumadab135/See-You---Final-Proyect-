using System.Collections;
using UnityEngine;

public class ZoneTransition : MonoBehaviour
{

    [SerializeField] private CameraConfinerController _cameraConfiner;
    [SerializeField] private Transform _targetPoint;
    [SerializeField] private Collider2D _newConfiner;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        StartCoroutine(TransitionRoutine(other.transform));
    }

    private IEnumerator TransitionRoutine(Transform playerTransform)
    {
        GameStateController.Instance.SetState(GameState.Cinematic);

        yield return FadeController.Instance.FadeOut();

        playerTransform.position = _targetPoint.position;

        SpriteRenderer sr = playerTransform.GetComponent<SpriteRenderer>();
        if (sr != null)
            sr.flipX = false;

        _cameraConfiner.SetConfiner(_newConfiner);

        yield return FadeController.Instance.FadeIn();

        GameStateController.Instance.SetState(GameState.Exploration);
    }
}

