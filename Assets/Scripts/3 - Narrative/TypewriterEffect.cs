using System.Collections;
using UnityEngine;
using TMPro;

public class TypewriterEffect : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _typeSound;
    private float _letterDelay;
    private float _bounceAmount;
    private float _bounceSpeed;
    private float _soundCooldown;


    private float _lastSoundTime;


    public bool IsTyping { get; private set; }

    private Coroutine _coroutine;

    public void Play(string text, TMP_Text textComponent, DialogueLine line)
    {
        _letterDelay = line.LetterDelay;
        _bounceAmount = line.BounceAmount;
        _bounceSpeed = line.BounceSpeed;
        _soundCooldown = line.SoundCooldown;

        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(TypeRoutine(text, textComponent));
    }

    public void Skip(string fullText, TMP_Text textComponent)
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        textComponent.text = fullText;
        IsTyping = false;
    }
    private void PlayTypeSound()
    {
        if (_audioSource == null || _typeSound == null) return;

        if (Time.time - _lastSoundTime < _soundCooldown) return;

        _audioSource.PlayOneShot(_typeSound);
        _lastSoundTime = Time.time;
    }

    private IEnumerator TypeRoutine(string text, TMP_Text textComponent)
    {
        IsTyping = true;
        textComponent.text = "";
        textComponent.ForceMeshUpdate();

        for (int i = 0; i < text.Length; i++)
        {
            textComponent.text += text[i];
            textComponent.ForceMeshUpdate();
            PlayTypeSound();

            int charIndex = i;
            TMP_TextInfo textInfo = textComponent.textInfo;
            if (charIndex < textInfo.characterCount)
            {
                TMP_CharacterInfo charInfo = textInfo.characterInfo[charIndex];
                if (!charInfo.isVisible) continue;

                Vector3[] verts = textInfo.meshInfo[charInfo.materialReferenceIndex].vertices;

                int vertexIndex = charInfo.vertexIndex;
                Vector3 offset = new Vector3(0, _bounceAmount, 0);

                Vector3 v0 = verts[vertexIndex + 0];
                Vector3 v1 = verts[vertexIndex + 1];
                Vector3 v2 = verts[vertexIndex + 2];
                Vector3 v3 = verts[vertexIndex + 3];

                float t = 0f;
                while (t < 1f)
                {
                    t += Time.deltaTime * _bounceSpeed;
                    float yOffset = Mathf.Sin(t * Mathf.PI) * _bounceAmount;

                    verts[vertexIndex + 0] = v0 + new Vector3(0, yOffset, 0);
                    verts[vertexIndex + 1] = v1 + new Vector3(0, yOffset, 0);
                    verts[vertexIndex + 2] = v2 + new Vector3(0, yOffset, 0);
                    verts[vertexIndex + 3] = v3 + new Vector3(0, yOffset, 0);

                    textComponent.UpdateVertexData(TMP_VertexDataUpdateFlags.Vertices);
                    yield return null;
                }
            }

            yield return new WaitForSeconds(_letterDelay);
        }

        IsTyping = false;
    }
}
