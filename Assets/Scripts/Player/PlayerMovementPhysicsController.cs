using UnityEngine;

public sealed class PlayerMovementPhysicsController : IFixedUpdate
{
    private PlayerView _view;
    private PlayerModel _model;
    private SpriteAnimator _spriteAnimator;
    private float _xAxisInput;
    private bool _doJump;
    private ContactsPoller _contactsPoller;

    public PlayerMovementPhysicsController(PlayerView view, PlayerModel model, SpriteAnimator animator)
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
    }

    public void FixedUpdate()
    {
        _contactsPoller.Update();

        bool isMovingSide = Mathf.Abs(_xAxisInput) > _model.MovingTreshold;
        bool isMovingLeft = _xAxisInput < 0f;
        if (isMovingSide)
        {
            _view.SpriteRenderer.flipX = isMovingLeft;
        }

        float newVelocity = 0f;
        if (isMovingSide && (isMovingLeft || !_contactsPoller.HasLeftContacts) && (isMovingLeft || !_contactsPoller.HasRightContacts))
        {
            float walkDirection = isMovingLeft ? -1f : 1f;
            newVelocity = Time.fixedDeltaTime * _model.WalkSpeed * walkDirection;
        }
        _view.Rigidbody.velocity = _view.Rigidbody.velocity.Change(x: newVelocity);

        if (_contactsPoller.IsGrounded && _doJump && Mathf.Abs(_view.Rigidbody.velocity.y) <= _model.JumpingTreshold)
        {
            _view.Rigidbody.AddForce(Vector3.up * _model.JumpForce);
        }

        if (_contactsPoller.IsGrounded)
        {
            var track = isMovingSide ? Track.Walk : Track.Idle;
            _spriteAnimator.StartAnimation(_view.SpriteRenderer, track, true);
        }
        else if (Mathf.Abs(_view.Rigidbody.velocity.y) > 1f)
        {
            var track = Track.Jump;
            _spriteAnimator.StartAnimation(_view.SpriteRenderer, track, true);
        }
    }
}