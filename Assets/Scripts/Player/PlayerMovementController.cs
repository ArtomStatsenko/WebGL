using UnityEngine;

public sealed class PlayerMovementController : IUpdate
{
    private PlayerView _view;
    private PlayerModel _model;
    private SpriteAnimator _spriteAnimator;
    private float _xAxisInput;
    private float _yVelocity;
    private float _groundLevel = 0f;
    private float _acceleration = -9.8f;
    private bool _doJump;

    public PlayerMovementController(PlayerView view, PlayerModel model, SpriteAnimator animator)
    {
        _view = view;
        _model = model;
        _spriteAnimator = animator;
    }

    public void Update()
    {
        _doJump = Input.GetAxis(Axis.VERTICAL) > _model.JumpingTreshold;
        _xAxisInput = Input.GetAxis(Axis.HORIZONTAL);

        bool isMovingSide = Mathf.Abs(_xAxisInput) > _model.MovingTreshold;
        if (isMovingSide)
        {
            MoveSide();
        }

        if (IsGrounded())
        {
            _spriteAnimator.StartAnimation(_view.SpriteRenderer, isMovingSide ? Track.Walk : Track.Idle, true);

            if (_doJump && Mathf.Approximately(_yVelocity, 0f))
            {
                _yVelocity = _model.JumpForce;
                _spriteAnimator.StartAnimation(_view.SpriteRenderer, Track.Jump, false);
            }
            else if (_yVelocity < 0f)
            {
                _yVelocity = 0f;
                _view.transform.position = _view.transform.position.Change(y: _groundLevel);
            }
        }
        else
        {
            _yVelocity += _acceleration * Time.deltaTime;
            _view.transform.position += Vector3.up * Time.deltaTime * _yVelocity;
        }
    }

    private void MoveSide()
    {
        Vector3 walkDirection = _xAxisInput < 0f ? Vector3.left : Vector3.right;
        _view.transform.position += walkDirection * Time.deltaTime * _model.WalkSpeed;
        bool flipX = _xAxisInput < 0f;
        _view.SpriteRenderer.flipX = flipX;
    }

    private bool IsGrounded()
    {
        return _view.transform.position.y <= _groundLevel + float.Epsilon && _yVelocity <= 0f;
    }
}
