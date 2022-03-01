using UnityEngine;

public sealed class PlayerMovementController : IUpdate
{
    private PlayerView _view;
    private PlayerModel _model;
    private float _xAxisInput = 0f;
    private float _yVelocity = 0f;
    private float _groundLevel = 0f;
    private float _acceleration = -9.8f;
    private bool _doJump = false;

    private PlayerAnimator _animator;

    public PlayerMovementController(PlayerView view, PlayerModel model, SpriteAnimator animator)
    {
        _view = view;
        _model = model;

        _animator = new PlayerAnimator(new Idle(), animator, view);
        _animator.IsJumping = false;
    }

    public void Update()
    {
        _doJump = Input.GetAxis(Axis.VERTICAL) > _model.JumpingTreshold;
        _xAxisInput = Input.GetAxis(Axis.HORIZONTAL);

        bool isMovingSide = Mathf.Abs(_xAxisInput) > _model.MovingTreshold;
        if (_animator.IsMoving != isMovingSide)
        {
            _animator.IsMoving = isMovingSide;
            _animator.Request();
        }
        if (isMovingSide)
        {
            MoveSide();
        }

        if (IsGrounded())
        {
            if (_doJump && Mathf.Approximately(_yVelocity, 0f))
            {
                Jump(true);
            }
            else if (_yVelocity < 0f)
            {
                Jump(false);
            }
        }
        else
        {
            _yVelocity += _acceleration * Time.deltaTime * _model.Mass;
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

    private void Jump(bool isJumping)
    {
        if (isJumping)
        {
            _yVelocity = _model.StartJumpSpeed;
        }
        else
        {
            _yVelocity = 0f;
            _view.transform.position = _view.transform.position.Change(y: _groundLevel);
        }

        _animator.IsJumping = isJumping;
        _animator.Request();
    }
}