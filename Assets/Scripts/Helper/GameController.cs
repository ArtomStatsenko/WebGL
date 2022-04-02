using Pathfinding;
using System.Collections.Generic;
using UnityEngine;

public sealed class GameController : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private BackgroundLayerView[] _backgrounds;
    [SerializeField] private SpriteAnimationsConfig _animationsConfig;
    [SerializeField] private PlayerView _playerView;
    [SerializeField] private PlayerModel _playerModel;
    [SerializeField] private CoinView[] _coinViews;
    [SerializeField] private EnemyView _jinnView;
    [SerializeField] private AIConfig _aiConfig;
    [SerializeField] private AIDestinationSetter _protectorAIDestinationSetter;
    [SerializeField] private AIPatrolPath _protectorAIPatrolPath;
    [SerializeField] private LevelObjectTrigger _protectedZoneTrigger;

    private BackgroundController _backgroundController;
    private SpriteAnimator _spriteAnimator;
    private PlayerMovementController _playerMovementController;
    private CameraController _cameraController;
    private CoinsManager _coinsManager;
    private DeadZoneManager _deadZoneManager;
    private ProtectorAI _protectorAi;
    private ProtectedZone _protectedZone;

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

        _protectorAi = new ProtectorAI(_jinnView, _aiConfig.Waypoints, _protectorAIDestinationSetter, _protectorAIPatrolPath, _spriteAnimator);
        _protectorAi.Init();

        _protectedZone = new ProtectedZone(_protectedZoneTrigger, new List<IProtector> { _protectorAi });
        _protectedZone.Init();
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
        _protectorAi.Dispose();
        _protectedZone.Dispose();
    }
}
