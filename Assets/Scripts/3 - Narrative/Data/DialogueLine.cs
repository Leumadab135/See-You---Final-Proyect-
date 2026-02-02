using UnityEngine;

[System.Serializable]
public class DialogueLine
{
    [TextArea(2, 5)]
    public string Text;

    public string SpeakerName;
    public Sprite Portrait;

    [Header("Delay Settings")]
    public float PreDelay;
    public float PostDelay;

    [Header("Typewriter Settings")]
    public float LetterDelay = 0.03f;
    public float BounceAmount = 10f;
    public float BounceSpeed = 5f;
    public float SoundCooldown = 0.03f;
}
