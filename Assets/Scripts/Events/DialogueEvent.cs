using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class DialogueEvent : NarrativeEvent
{
    [SerializeField] private DialogueData _dialogue;
    [SerializeField] private float _delayBeforeTalking;

    protected override IEnumerator EventRoutine()
    {
        GameStateController.Instance.SetState(GameState.Cinematic);

        yield return new WaitForSeconds(_delayBeforeTalking);

        if (_dialogue == null)
            yield break;

        bool finished = false;

        DialogueController.Instance.OnDialogueFinished += OnDialogueFinished;

        DialogueController.Instance.StartDialogue(_dialogue);

        while (!finished)
            yield return null;

        DialogueController.Instance.OnDialogueFinished -= OnDialogueFinished;

        void OnDialogueFinished(DialogueData data)
        {
            if (data == _dialogue)
                finished = true;
        }
    }
}
