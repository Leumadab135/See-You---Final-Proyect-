using System.Collections;
using UnityEngine;

public abstract class NarrativeEvent : MonoBehaviour
{
    public void Play()
    {
        StartCoroutine(EventRoutine());
    }

    protected abstract IEnumerator EventRoutine();
}
