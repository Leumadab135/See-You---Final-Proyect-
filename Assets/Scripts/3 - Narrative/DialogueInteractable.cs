using UnityEngine;

public class DialogueInteractable : MonoBehaviour
{
    [SerializeField] private DialogueData _dialogue;

    private bool _playerInRange;

    private void Update()
    {
        if (!_playerInRange) return;
        if (DialogueController.Instance.IsActive) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            DialogueController.Instance.StartDialogue(_dialogue);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            _playerInRange = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            _playerInRange = false;
    }
}
