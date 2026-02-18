using System.Collections;
using UnityEngine;

public class BirdEvent : NarrativeEvent
{
    [SerializeField] private GameObject _pidgeons;
    [SerializeField] private Animator _animator1;
    [SerializeField] private Animator _animator2;
    [SerializeField] private Animator _animator3;
    [SerializeField] private AudioSource _flyingPidgeon;

    [Header("Movement")]
    [SerializeField] private Vector2 _direction = new Vector2(1f, 1f);
    [SerializeField] private float _distance = 6f;
    [SerializeField] private float _duration = 1.5f;

    protected override IEnumerator EventRoutine()
    {
        _animator1.SetTrigger("Fly");
        _animator2.SetTrigger("Fly");
        _animator3.SetTrigger("Fly");

        _flyingPidgeon.Play();

        Vector3 start = _pidgeons.transform.position;
        Vector3 end = start + (Vector3)(_direction.normalized * _distance);

        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime / _duration;
            _pidgeons.transform.position = Vector3.Lerp(start, end, t);
            yield return null;
        }
    }
}
