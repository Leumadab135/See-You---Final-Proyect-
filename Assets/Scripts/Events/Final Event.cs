using System.Collections;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;

public class FinalEvent : NarrativeEvent
{
    [Header("Camera")]
    [SerializeField] private CameraConfinerController _cameraConfiner;
    [SerializeField] private Collider2D _newConfiner;
    [SerializeField] private CinemachineVirtualCameraBase _vCamBase;

    [Header("Girl")]
    [SerializeField] private GameObject _girl;

    [Header("Audio")]
    [SerializeField] private AudioSource _revelationFX;
    [SerializeField] private AudioSource _musicToPlay;
    [SerializeField] private AudioSource _musicToStop;

    [Header("Last settings")]
    [SerializeField] private GameObject _blackBackground;
    [SerializeField] private GameObject _title;
    [SerializeField] private DialogueData _dialogue;


    protected override IEnumerator EventRoutine()
    {
        GameStateController.Instance.SetState(GameState.Cinematic);

        _revelationFX.Play();

        yield return new WaitForSeconds(3f);
        _musicToStop.Stop();

        yield return new WaitForSeconds(0.3f);
        _blackBackground.SetActive(true);

        yield return new WaitForSeconds(2f);
        _cameraConfiner.SetConfiner(_newConfiner);
        _vCamBase.Follow = _girl.transform;

        yield return new WaitForSeconds(2f);
        _blackBackground.SetActive(false);
        yield return FadeController.Instance.FadeIn();

        yield return new WaitForSeconds(3.6f);
        DialogueController.Instance.StartDialogue(_dialogue);

        yield return new WaitForSeconds(12f);
        yield return FadeController.Instance.FadeOut();


        yield return new WaitForSeconds(2f);
        StartCoroutine(PlayMusicWithFade());

        yield return new WaitForSeconds(0.5f);
        StartCoroutine(Title());
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
        Vector3 targetScale = originalScale * 1.1f; // Crece 10%

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

        //while (time < duration / 2f)
        //{
        //    time += Time.deltaTime;
        //    float progress = time / (duration / 2f);
        //    titleTransform.localScale = Vector3.Lerp(targetScale, originalScale, progress);
        //    yield return null;
        //}

        //titleTransform.localScale = originalScale;
    }
}
