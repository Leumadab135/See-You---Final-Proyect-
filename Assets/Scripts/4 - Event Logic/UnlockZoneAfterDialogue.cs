using UnityEngine;

public class UnlockZoneAfterDialogue : MonoBehaviour
{
    [SerializeField] private DialogueData _dialogueToListen;
    [SerializeField] private GameObject _zoneToEnable;

    private void Start()
    {
        if (DialogueController.Instance == null)
        {
            Debug.LogError("DialogueController not found in scene.");
            return;
        }

        DialogueController.Instance.OnDialogueFinished += OnDialogueFinished;
    }

    private void OnDestroy()
    {
        if (DialogueController.Instance != null)
            DialogueController.Instance.OnDialogueFinished -= OnDialogueFinished;
    }

    private void OnDialogueFinished(DialogueData dialogue)
    {
        if (dialogue != _dialogueToListen) return;

        if (_zoneToEnable != null)
            _zoneToEnable.SetActive(true);
    }
}
