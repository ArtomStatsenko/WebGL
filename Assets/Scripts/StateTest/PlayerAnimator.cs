using UnityEngine;

public sealed class PlayerAnimator
{
    private State _state;
    private SpriteAnimator _spriteAnimator;
    private PlayerView _playerView;

    public bool IsJumping { get; set; }
    public bool IsMoving { get; set; }
    public State State
    {
        set
        {
            _state = value;
            Debug.Log($"State: {_state.GetType().Name}");
        }
    }

    public PlayerAnimator(State state, SpriteAnimator spriteAnimator, PlayerView playerView)
    {
        _state = state;
        _spriteAnimator = spriteAnimator;
        _playerView = playerView;
    }

    public void Request()
    {
        _state.Handle(this);
        
        if (_state is Walk)
        {
            _spriteAnimator.StartAnimation(_playerView.SpriteRenderer, Track.Walk, true);
        }
        else if(_state is Jump)
        {
            _spriteAnimator.StartAnimation(_playerView.SpriteRenderer, Track.Jump, false);
        }
        else
        {
            _spriteAnimator.StartAnimation(_playerView.SpriteRenderer, Track.Idle, true);
        }

    }
}