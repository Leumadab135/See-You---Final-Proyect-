using System.Collections;
using TMPro;
using UnityEngine;

public class DayCountPanelEvent : NarrativeEvent
{
    [SerializeField] private GameObject _blackBackground;
    [SerializeField] private TextMeshProUGUI _dayText;
    [SerializeField] private AudioSource _dramaticSoundFX;
    [SerializeField] private AudioSource _musicToPlay;
    [SerializeField] private AudioSource _musicToStop;
    protected override IEnumerator EventRoutine()
    {
        GameStateController.Instance.SetState(GameState.Cinematic);

        if (_musicToStop != null)
            _musicToStop.Stop();

        _blackBackground.SetActive(true);

        yield return new WaitForSeconds(1f);
        _dayText.gameObject.SetActive(true);
        _dramaticSoundFX.Play();

        yield return new WaitForSeconds(2);
        _musicToPlay.Play();

        yield return new WaitForSeconds(3);
        _blackBackground.SetActive(false);
        _dayText.gameObject.SetActive(false);

        while (DialogueController.Instance.IsActive)
            yield return null;

        GameStateController.Instance.SetState(GameState.Exploration);
    }

    public static IEnumerator FadeOut(AudioSource source, float duration)
    {
        if (source == null || !source.isPlaying)
            yield break;

        float startVolume = source.volume;
        float t = 0f;

        while (t < duration)
        {
            t += Time.deltaTime;
            source.volume = Mathf.Lerp(startVolume, 0f, t / duration);
            yield return null;
        }

        source.volume = 0f;
        source.Stop();
    }
}
