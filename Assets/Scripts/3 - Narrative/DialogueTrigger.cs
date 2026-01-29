using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private DialogueData _dialogue;
    [SerializeField] private bool _triggerOnce = true;

    private bool _hasTriggered;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_hasTriggered && _triggerOnce) return;
        if (!other.CompareTag("Player")) return;
        if (DialogueController.Instance.IsActive) return;

        DialogueController.Instance.StartDialogue(_dialogue);
        _hasTriggered = true;
    }
}
