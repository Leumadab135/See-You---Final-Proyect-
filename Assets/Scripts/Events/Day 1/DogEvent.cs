using System.Collections;
using UnityEngine;

public class DogEvent : NarrativeEvent
{
    [SerializeField] private GameObject _dog;
    [SerializeField] private Animator _animator;
    [SerializeField] private AudioSource _dogRun;

    [Header("Movement")]
    [SerializeField] private float _distance = 6f;
    [SerializeField] private float _duration = 1.5f;
    [SerializeField] private bool _flipSprite = true;

    protected override IEnumerator EventRoutine()
    {
        if (_dog == null) yield break;

        Vector3 startPos = _dog.transform.position;
        Vector3 endPos = startPos + Vector3.right * _distance;

        // Flip visual
        if (_flipSprite)
        {
            Vector3 scale = _dog.transform.localScale;
            scale.x *= -1f;
            _dog.transform.localScale = scale;
        }

        // Animación
        if (_animator != null)
            _animator.SetTrigger("Run");

        // Sonido
        if (_dogRun != null)
            _dogRun.Play();

        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime / _duration;
            _dog.transform.position = Vector3.Lerp(startPos, endPos, t);
            yield return null;
        }
    }
}

