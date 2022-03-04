using UnityEngine;

public sealed class GameController : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private BackgroundLayerView[] _backgrounds;
    [SerializeField] private SpriteAnimationsConfig _animationsConfig;
    [SerializeField] private PlayerView _playerView;
    [SerializeField] private PlayerModel _playerModel;
    [SerializeField] private CoinView[] _coinViews;

    private BackgroundController _backgroundController;
    private SpriteAnimator _spriteAnimator;
    private PlayerMovementController _playerMovementController;
    private CameraController _cameraController;
    private CoinsManager _coinsManager;
    private DeadZoneManager _deadZoneManager;

    private void Start()
    {
        _spriteAnimator = new SpriteAnimator(_animationsConfig);
        _backgroundController = new BackgroundController(_camera.transform, _backgrounds)
        {
            IsParallax = true,
            IsEndless = true
        };
        _playerMovementController = new PlayerMovementController(_playerView, _playerModel, _spriteAnimator);
        _cameraController = new CameraController(_camera.transform, _playerView.transform);
        _coinsManager = new CoinsManager(_playerView, _coinViews, _spriteAnimator);
        _deadZoneManager = new DeadZoneManager(_playerView);
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
        _spriteAnimator.Dispose();
        _coinsManager.Dispose();
        _deadZoneManager.Dispose();
    }
}
