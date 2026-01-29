using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float Horizontal { get; private set; }

    private void Update()
    {
        if (!GameStateController.Instance.IsExploration())
        {
            Horizontal = 0f;
            return;
        }

        Horizontal = Input.GetAxis("Horizontal");
    }
}

