using System.Collections;
using TMPro;
using UnityEngine;

public class Day1PanelEvent : NarrativeEvent
{
    [SerializeField] private GameObject _blackBackground; 
    [SerializeField] private TextMeshProUGUI _day1Text; 
    [SerializeField] private AudioSource _dramaticSoundFX;
    [SerializeField] private AudioSource _musicTheButterfly;
    protected override IEnumerator EventRoutine()
    {
        GameStateController.Instance.SetState(GameState.Cinematic);

        _blackBackground.SetActive(true);

        yield return new WaitForSeconds(1f);
        _day1Text.gameObject.SetActive(true);
        _dramaticSoundFX.Play();

        yield return new WaitForSeconds(2);
        _musicTheButterfly.Play();

        yield return new WaitForSeconds(3);
        _blackBackground.SetActive(false);
        _day1Text.gameObject.SetActive(false);

        while (DialogueController.Instance.IsActive)
            yield return null;

        GameStateController.Instance.SetState(GameState.Exploration);
    }
}
