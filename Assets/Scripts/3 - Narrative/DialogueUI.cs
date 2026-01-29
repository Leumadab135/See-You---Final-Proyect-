using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private Image _portraitImage;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _dialogueText;

    [SerializeField] private float _portraitFadeDuration = 0.2f;
    [SerializeField] private float _showDuration = 0.2f;

    private Coroutine _portraitFadeRoutine;
    public TextMeshProUGUI DialogueText => _dialogueText;

    public void Show()
    {
        _panel.SetActive(true);
    }

    public void Hide()
    {
        _panel.SetActive(false);
        _dialogueText.text = string.Empty;
    }

    public void SetLine(DialogueLine line)
    {
        _nameText.text = line.SpeakerName;
        _dialogueText.text = string.Empty;

        if (_portraitFadeRoutine != null)
            StopCoroutine(_portraitFadeRoutine);

        _portraitFadeRoutine = StartCoroutine(FadePortrait(line.Portrait));
    }

    private IEnumerator FadePortrait(Sprite newPortrait)
    {
        if (_portraitImage.sprite != null)
        {
            yield return FadeAlpha(1f, 0f);
        }

        _portraitImage.sprite = newPortrait;
        _portraitImage.gameObject.SetActive(newPortrait != null);

        if (newPortrait != null)
        {
            yield return FadeAlpha(0f, 1f);
        }
    }

    private IEnumerator FadeAlpha(float from, float to)
    {
        float t = 0f;
        Color c = _portraitImage.color;

        while (t < _portraitFadeDuration)
        {
            t += Time.deltaTime;
            c.a = Mathf.Lerp(from, to, t / _portraitFadeDuration);
            _portraitImage.color = c;
            yield return null;
        }

        c.a = to;
        _portraitImage.color = c;
    }
}
