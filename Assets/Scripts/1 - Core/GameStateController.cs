using UnityEngine;

public class GameStateController : MonoBehaviour
{
    public static GameStateController Instance { get; private set; }

    public GameState CurrentState { get; private set; }

    public event System.Action<GameState> OnStateChanged;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        SetState(GameState.Exploration);
    }

    public void SetState(GameState newState)
    {
        if (CurrentState == newState) return;

        CurrentState = newState;
        OnStateChanged?.Invoke(CurrentState);
    }

    public bool IsExploration()
    {
        return CurrentState == GameState.Exploration;
    }
}

