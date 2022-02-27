using UnityEngine;

public sealed class GameController : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private BackgroundLayerView[] _backgrounds;
    [SerializeField] private SpriteAnimationsConfig _animationsConfig;
    [SerializeField] private PlayerView _playerView;
    [SerializeField] private PlayerModel _playerModel;
    [SerializeField] private CannonView _cannonView;
    [SerializeField] private BulletView _bulletView;
    [SerializeField] private BulletModel _bulletModel;

    private BackgroundController _backgroundController;
    private SpriteAnimator _spriteAnimator;
    private PlayerMovementController _playerMovementController;
    private CannonController _cannonController;
    private BulletsEmitter _bulletsEmitter;

    private void Start()
    {
        _spriteAnimator = new SpriteAnimator(_animationsConfig);
        _backgroundController = new BackgroundController(_camera.transform, _backgrounds)
        {
            IsParallax = true,
            IsEndless = true
        };
        _playerMovementController = new PlayerMovementController(_playerView, _playerModel, _spriteAnimator);
        _cannonController = new CannonController(_cannonView.Muzzle, _playerView.transform);
        _bulletsEmitter = new BulletsEmitter(_bulletModel, _bulletView.gameObject, _cannonView.Muzzle.transform);
    }

    private void Update()
    {
        _spriteAnimator.Update();
        _backgroundController.Update();
        _playerMovementController.Update();
        _cannonController.Update();
        _bulletsEmitter.Update();
    }

    private void FixedUpdate()
    {
        
    }

    private void OnDestroy()
    {
        
    }
}
