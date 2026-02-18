using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeController : MonoBehaviour
{
    public static FadeController Instance { get; private set; }
    [SerializeField] private Image _fadeImage;
    [SerializeField] private float _fadeDuration = 1f;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public IEnumerator FadeOut()
    {
        yield return Fade(0f, 2f);
    }

    public IEnumerator FadeIn()
    {
        yield return Fade(10f, 0f);
    }

    private IEnumerator Fade(float from, float to)
    {
        _fadeImage.gameObject.SetActive(true);
        float t = 0f;
        Color color = _fadeImage.color;

        while (t < 1f)
        {
            t += Time.deltaTime / _fadeDuration;
            color.a = Mathf.Lerp(from, to, t);
            _fadeImage.color = color;
            yield return null;
        }

        color.a = to;
        _fadeImage.color = color;
        _fadeImage.gameObject.SetActive(true);

    }
}

