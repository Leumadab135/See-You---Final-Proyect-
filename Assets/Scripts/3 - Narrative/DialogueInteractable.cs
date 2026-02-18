using UnityEngine;

public class DialogueInteractable : MonoBehaviour
{
    [SerializeField] private DialogueData _dialogue;

    private bool _playerInRange;
    private bool _used;

    private void Update()
    {
        if (_used) return;
        if (!_playerInRange) return;
        if (DialogueController.Instance.IsActive) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            DialogueController.Instance.StartDialogue(_dialogue);
            _used = true;
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
