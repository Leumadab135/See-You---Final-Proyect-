using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement2D : MonoBehaviour
{
    [SerializeField] private float _speed = 3f;

    private Rigidbody2D _rb;
    private PlayerInput _input;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _input = GetComponent<PlayerInput>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector2 velocity = _rb.linearVelocity;
        velocity.x = _input.Horizontal * _speed;
        _rb.linearVelocity = velocity;
    }
}

