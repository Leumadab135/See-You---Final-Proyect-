using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerAnimator2D : MonoBehaviour
{
    private static readonly int IsWalking = Animator.StringToHash("IsWalking");

    private Animator _animator;
    private Rigidbody2D _rb;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        UpdateMovementAnimation();
        UpdateFacingDirection();
    }

    private void UpdateMovementAnimation()
    {
        bool isWalking = Mathf.Abs(_rb.linearVelocity.x) > 0.01f;
        _animator.SetBool(IsWalking, isWalking);
    }

    private void UpdateFacingDirection()
    {
        if (_rb.linearVelocity.x > 0.01f)
            _spriteRenderer.flipX = false;
        else if (_rb.linearVelocity.x < -0.01f)
            _spriteRenderer.flipX = true;
    }
}

