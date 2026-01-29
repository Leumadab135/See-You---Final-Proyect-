//using UnityEngine;

//public class NarrativeLock : MonoBehaviour
//{
//    private void OnEnable()
//    {
//        DialogueController.Instance.OnDialogueStarted += LockGameplay;
//        DialogueController.Instance.OnDialogueEnded += UnlockGameplay;
//    }

//    private void OnDisable()
//    {
//        DialogueController.Instance.OnDialogueStarted -= LockGameplay;
//        DialogueController.Instance.OnDialogueEnded -= UnlockGameplay;
//    }

//    private void LockGameplay()
//    {
//        GameStateController.Instance.SetState(GameState.Dialogue);
//    }

//    private void UnlockGameplay()
//    {
//        GameStateController.Instance.SetState(GameState.Exploration);
//    }
//}
