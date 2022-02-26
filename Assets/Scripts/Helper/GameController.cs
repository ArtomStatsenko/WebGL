using UnityEngine;

public sealed class GameController : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private ParalaxBackground[] _backgrounds;
    [SerializeField] private SpriteAnimationsConfig _animationsConfig;
    [SerializeField] private PlayerView _playerView;
    [SerializeField] private PlayerModel _playerModel;

    private ParalaxController _paralaxController;
    private SpriteAnimator _spriteAnimator;
    private PlayerMovementController _playerMovementController;

    private void Start()
    {
        _spriteAnimator = new SpriteAnimator(_animationsConfig);
        _paralaxController = new ParalaxController(_camera.transform, _backgrounds);
        _playerMovementController = new PlayerMovementController(_playerView, _playerModel, _spriteAnimator);
    }

    private void Update()
    {
        _spriteAnimator.Update();
        _paralaxController.Update();
        _playerMovementController.Update();
    }

    private void FixedUpdate()
    {
        
    }

    private void OnDestroy()
    {
        
    }
}
