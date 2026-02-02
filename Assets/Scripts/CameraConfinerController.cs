using Unity.Cinemachine;
using UnityEngine;

public class CameraConfinerController : MonoBehaviour
{
    [SerializeField] private CinemachineConfiner2D _confiner;

    public void SetConfiner(Collider2D newBounds)
    {
        _confiner.BoundingShape2D = newBounds;
    }
}

