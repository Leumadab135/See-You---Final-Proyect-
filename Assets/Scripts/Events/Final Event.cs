using System.Collections;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class FinalEvent : NarrativeEvent
{
    [Header("Camera")]
    [SerializeField] private CameraConfinerController _cameraConfiner;
    [SerializeField] private Collider2D _newConfiner;
    [SerializeField] private CinemachineVirtualCameraBase _vCamBase;

    [Header("Girl")]
    [SerializeField] private GameObject _girl;
    [SerializeField] private DialogueData _dialogue;

    [Header("Audio")]
    [SerializeField] private AudioSource _revelationFX;
    [SerializeField] private AudioSource _parkAudio;
    [SerializeField] private AudioSource _musicToPlay;
    [SerializeField] private AudioSource _musicToStop;

    [Header("Final Scene")]
    [SerializeField] private GameObject _blackBackground;
    [SerializeField] private GameObject _title;
    [SerializeField] private TextMeshProUGUI _finalText;
    [SerializeField] private float _fadeDuration = 10f;
    [SerializeField] private Button _exitButton;



    private void Awake()
    {
        Color color = _finalText.color;
        color.a = 0f;
        _finalText.color = color;
    }

    protected override IEnumerator EventRoutine()
    {
        GameStateController.Instance.SetState(GameState.Cinematic);

        yield return new WaitForSeconds(3.4f);
        _revelationFX.Play();
        _musicToStop.Stop();
        _blackBackground.SetActive(true);

        yield return new WaitForSeconds(2f);
        _cameraConfiner.SetConfiner(_newConfiner);
        _vCamBase.Follow = _girl.transform;

        yield return new WaitForSeconds(2f);
        _blackBackground.SetActive(false);
        yield return FadeController.Instance.FadeIn();
        _parkAudio.Play();


        yield return new WaitForSeconds(3.6f);
        DialogueController.Instance.StartDialogue(_dialogue);

        yield return new WaitForSeconds(12f);
        yield return FadeController.Instance.FadeOut();


        yield return new WaitForSeconds(2f);
        StartCoroutine(PlayMusicWithFade());

        yield return new WaitForSeconds(0.5f);
        _parkAudio.Stop();
        StartCoroutine(Title());

        yield return new WaitForSeconds(10.5f);
        _title.SetActive(false);
        yield return StartCoroutine(FadeInText());

        yield return new WaitForSeconds(10f);
        _exitButton.gameObject.SetActive(true);
    }

    private IEnumerator PlayMusicWithFade()
    {
        _musicToPlay.volume = 0f;
        _musicToPlay.Play();

        float duration = 1.5f;
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            _musicToPlay.volume = Mathf.Lerp(0f, 1f, time / duration);
            yield return null;
        }

        _musicToPlay.volume = 1f;
    }

    private IEnumerator Title()
    {
        _title.SetActive(true);
        Transform titleTransform = _title.transform;

        Vector3 originalScale = titleTransform.localScale;
        Vector3 targetScale = originalScale * 1.3f;

        float duration = 20f;
        float time = 0f;

        while (time < duration / 2f)
        {
            time += Time.deltaTime;
            float progress = time / (duration / 2f);
            titleTransform.localScale = Vector3.Lerp(originalScale, targetScale, progress);
            yield return null;
        }

        time = 0f;
    }

    private IEnumerator FadeText(float targetAlpha)
    {
        float startAlpha = _finalText.color.a;
        float time = 0f;

        Color color = _finalText.color;

        while (time < _fadeDuration)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, time / _fadeDuration);

            color.a = alpha;
            _finalText.color = color;

            yield return null;
        }

        color.a = targetAlpha;
        _finalText.color = color;
    }

    private IEnumerator FadeInText()
    {
        yield return StartCoroutine(FadeText(1f));
    }

    //private IEnumerator FadeOutText()
    //{
    //    yield return StartCoroutine(FadeText(0f));
    //}
}
