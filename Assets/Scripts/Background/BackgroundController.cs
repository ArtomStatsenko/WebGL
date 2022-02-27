using UnityEngine;

public sealed class BackgroundController : IUpdate
{
    private Transform _camera;
    private BackgroundLayerView[] _backgrounds;
    private Vector3[] _backgroundsStartPositions;
    private Vector3 _cameraStartPosition;
    private Vector3 _direction;
    private Vector3 _leftSpritePosition;
    private Vector3 _rightSpritePosition;
    private bool _isParallax;
    private bool _isEndless;

    public bool IsParallax
    {
        get => _isParallax;
        set => _isParallax = value;
    }
    public bool IsEndless
    {
        get => _isEndless;
        set => _isEndless = value;
    }

    public BackgroundController(Transform camera, BackgroundLayerView[] backgrounds)
    {
        _camera = camera;
        _backgrounds = backgrounds;
        _cameraStartPosition = _camera.transform.position;
        _backgroundsStartPositions = new Vector3[backgrounds.Length];

        for (int i = 0; i < backgrounds.Length; i++)
        {
            _backgroundsStartPositions[i] = backgrounds[i].transform.position;
        }
    }

    public void Update()
    {
        _direction = _camera.position - _cameraStartPosition;
        for (int i = 0; i < _backgrounds.Length; i++)
        {
            if (_isParallax)
            {
                _backgrounds[i].transform.position = _backgroundsStartPositions[i] + _direction * _backgrounds[i].SpeedCoef;
            }
            if (_isEndless)
            {
                RepeatBackround(_backgrounds[i]);
            }
        }
    }

    private void RepeatBackround(BackgroundLayerView background)
    {
        float movingOffsetX = background.MovingOffsetX;
        _leftSpritePosition = background.Left.position;
        _rightSpritePosition = background.Right.position;

        if (_camera.position.x > _rightSpritePosition.x)
        {
            background.Left.position = _leftSpritePosition.Change(x: _leftSpritePosition.x + movingOffsetX);
            background.SwapTransforms();
        }

        if (_camera.position.x < _leftSpritePosition.x)
        {
            background.Right.position = _rightSpritePosition.Change(x: _rightSpritePosition.x - movingOffsetX);
            background.SwapTransforms();
        }
    }
}
