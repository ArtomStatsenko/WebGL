using UnityEngine;

public sealed class GameController : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private ParalaxMember[] _backgrounds;
    [SerializeField] private PlayerView _playerView;

    private const string SPRITE_ANIMATION_CONFIG_PATH = "SpriteAnimationConfig";
    private ParalaxController _paralaxController;
    private SpriteAnimator _spriteAnimator;

    private void Start()
    {
        SpriteAnimationsConfig animationsConfig = Resources.Load<SpriteAnimationsConfig>(SPRITE_ANIMATION_CONFIG_PATH);
        _spriteAnimator = new SpriteAnimator(animationsConfig);
        _spriteAnimator.StartAnimation(_playerView.SpriteRenderer, Track.Idle, true, 12);

        _paralaxController = new ParalaxController(_camera.transform, _backgrounds);
    }

    private void Update()
    {
        _spriteAnimator.Update();
        _paralaxController.Update();
    }

    private void FixedUpdate()
    {
        
    }

    private void OnDestroy()
    {
        
    }
}
