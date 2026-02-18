using System.Collections;
using UnityEngine;

public class DialogueEvent : NarrativeEvent
{
    [SerializeField] private DialogueData _dialogue;

    protected override IEnumerator EventRoutine()
    {
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
