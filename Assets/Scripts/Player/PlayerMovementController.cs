using UnityEngine;

public sealed class PlayerMovementController : IUpdate, IFixedUpdate
{
    private PlayerView _view;
    private PlayerModel _model;
    private SpriteAnimator _spriteAnimator;
    private ContactsPoller _contactsPoller;
    private float _xAxisInput;
    private bool _doJump;
    private bool _isMovingSide;

    public PlayerMovementController(PlayerView view, PlayerModel model, SpriteAnimator animator)
    {
        _view = view;
        _model = model;
        _spriteAnimator = animator;
        _contactsPoller = new ContactsPoller(view.Collider);
    }

    public void Update()
    {
        _doJump = Input.GetAxis(Axis.VERTICAL) > _model.JumpingTreshold;
        _xAxisInput = Input.GetAxis(Axis.HORIZONTAL);
        _isMovingSide = Mathf.Abs(_xAxisInput) > _model.MovingTreshold;

        Animate();
    }

    public void FixedUpdate()
    {
        _contactsPoller.Update();

        bool isMovingLeft = _xAxisInput < 0f;
        if (_isMovingSide)
        {
            _view.SpriteRenderer.flipX = isMovingLeft;
        }

        float newVelocity = 0f;
        if (_isMovingSide && (!isMovingLeft || !_contactsPoller.HasLeftContacts) && (isMovingLeft || !_contactsPoller.HasRightContacts))
        {
            float walkDirection = isMovingLeft ? -1f : 1f;
            newVelocity = Time.fixedDeltaTime * _model.WalkSpeed * walkDirection;
        }
        _view.Rigidbody.velocity = _view.Rigidbody.velocity.Change(x: newVelocity);

        if (_contactsPoller.IsGrounded && _doJump && Mathf.Abs(_view.Rigidbody.velocity.y) <= _model.JumpingTreshold)
        {
            _view.Rigidbody.AddForce(Vector3.up * _model.JumpForce);
        }
    }

    private void Animate()
    {
        if (_contactsPoller.IsGrounded)
        {
            Track track = _isMovingSide ? Track.Walk : Track.Idle;
            _spriteAnimator.StartAnimation(_view.SpriteRenderer, track, true);
        }
        else if (Mathf.Abs(_view.Rigidbody.velocity.y) > 1f)
        {
            _spriteAnimator.StartAnimation(_view.SpriteRenderer, Track.Jump, false);
        }
    }
}