using UnityEngine;

[System.Serializable]
public class DialogueLine
{
    [TextArea(2, 5)]
    public string Text;

    public string SpeakerName;
    public Sprite Portrait;

    [Tooltip("Pausa antes de empezar a escribir")]
    public float PreDelay;

    [Tooltip("Pausa después de terminar la línea")]
    public float PostDelay;
}
