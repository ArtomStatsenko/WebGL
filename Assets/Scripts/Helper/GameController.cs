using UnityEngine;

public sealed class GameController : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private BackgroundLayerView[] _backgrounds;
    [SerializeField] private SpriteAnimationsConfig _animationsConfig;
    [SerializeField] private PlayerView _playerView;
    [SerializeField] private PlayerModel _playerModel;

    private BackgroundController _backgroundController;
    private SpriteAnimator _spriteAnimator;
    private PlayerMovementPhysicsController _playerMovementController;
    private CameraController _cameraController;

    private void Start()
    {
        _spriteAnimator = new SpriteAnimator(_animationsConfig);
        _backgroundController = new BackgroundController(_camera.transform, _backgrounds)
        {
            IsParallax = true,
            IsEndless = true
        };
        _playerMovementController = new PlayerMovementPhysicsController(_playerView, _playerModel, _spriteAnimator);
        _cameraController = new CameraController(_camera.transform, _playerView.transform);
    }

    private void Update()
    {
        _spriteAnimator.Update();
        _backgroundController.Update();
        _playerMovementController.Update();
    }

    private void FixedUpdate()
    {
        _playerMovementController.FixedUpdate();
    }

    private void LateUpdate()
    {
        _cameraController.LateUpdate();
    }

    private void OnDestroy()
    {
        
    }
}
