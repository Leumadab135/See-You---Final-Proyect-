using System.Collections;
using UnityEngine;

public class AudioAmbientController : MonoBehaviour
{
    public static AudioAmbientController Instance { get; private set; }

    [SerializeField] private AudioSource _source;
    [SerializeField] private float _fadeTime = 1.5f;

    private Coroutine _fadeRoutine;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void PlayAmbient(AudioClip clip, float volume = 1f)
    {
        if (_source.clip == clip) return;

        if (_fadeRoutine != null)
            StopCoroutine(_fadeRoutine);

        _fadeRoutine = StartCoroutine(FadeRoutine(clip, volume));
    }

    private IEnumerator FadeRoutine(AudioClip newClip, float targetVolume)
    {
        float startVol = _source.volume;

        // Fade out
        while (_source.volume > 0)
        {
            _source.volume -= Time.deltaTime / _fadeTime;
            yield return null;
        }

        _source.Stop();
        _source.clip = newClip;

        if (newClip != null)
        {
            _source.Play();

            // Fade in
            while (_source.volume < targetVolume)
            {
                _source.volume += Time.deltaTime / _fadeTime;
                yield return null;
            }
        }
    }
}
