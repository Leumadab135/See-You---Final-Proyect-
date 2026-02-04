using UnityEngine;

public class EventTrigger : MonoBehaviour
{
    [SerializeField] private NarrativeEvent _event;
    [SerializeField] private bool _playOnce = true;

    private bool _played;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_played && _playOnce) return;
        if (!other.CompareTag("Player")) return;

        _played = true;
        _event.Play();
    }
}
