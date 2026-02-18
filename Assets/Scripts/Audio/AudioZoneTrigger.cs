using UnityEngine;

public class AudioZoneTrigger : MonoBehaviour
{
    [SerializeField] private AudioClip _ambientClip;
    [SerializeField] private float _volume = 1f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        if (AudioAmbientController.Instance != null)
            AudioAmbientController.Instance.PlayAmbient(_ambientClip, _volume);
    }
}
