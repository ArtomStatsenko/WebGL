using System.Collections.Generic;
using UnityEngine;

public sealed class Animation
{
    private List<Sprite> _sprites;
    private Track _track;
    private bool _isLoop = false;
    private bool _isSleep;
    private float _speed;
    private float _counter = 0f;

    public List<Sprite> Sprites
    {
        get => _sprites;
        set => _sprites = value;
    }
    public Track Track
    {
        get => _track;
        set => _track = value;
    }
    public bool IsLoop
    {
        get => _isLoop;
        set => _isLoop = value;
    }
    public bool IsSleep
    {
        get => _isSleep;
        set => _isSleep = value;
    }
    public float Speed
    {
        get => _speed;
        set => _speed = value;
    }
    public float Counter
    {
        get => _counter;
        private set => _counter = value;
    }

    public void Update()
    {
        if(_isSleep)
        {
            return;
        }

        _counter += Time.deltaTime * _speed;

        if (_isLoop)
        {
            while (_counter > _sprites.Count)
            {
                _counter -= _sprites.Count;
            }
        }
        else if (_counter > _sprites.Count)
        {
            _counter = _sprites.Count - 1;
            _isSleep = true;
        }
    }

    public void ResetCounter()
    {
        _counter = 0f;
    }
}