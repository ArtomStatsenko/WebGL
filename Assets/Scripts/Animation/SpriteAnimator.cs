using System;
using System.Collections.Generic;
using UnityEngine;

public sealed class SpriteAnimator : IDisposable
{
    private SpriteAnimationsConfig _config;
    private Dictionary<SpriteRenderer, Animation> _activeAnimation = new Dictionary<SpriteRenderer, Animation>();

    public SpriteAnimator(SpriteAnimationsConfig config)
    {
        _config = config;
    }

    public void StartAnimation(SpriteRenderer spriteRenderer, Track track, bool isLoop, float speed)
    {
        if(_activeAnimation.TryGetValue(spriteRenderer, out var animation))
        {
            animation.IsLoop = isLoop;
            animation.IsSleep = false;
            animation.Speed = speed;

            if (animation.Track != track)
            {
                animation.Track = track;
                animation.Sprites = _config.Sequences.Find(sequence => sequence.Track == track).Sprites;
                animation.ResetCounter();
            }
        }
        else
        {
            _activeAnimation.Add(spriteRenderer, new Animation()
            {
                Track = track,
                Sprites = _config.Sequences.Find(sequence => sequence.Track == track).Sprites,
                IsLoop = isLoop,
                Speed = speed
            });
        }
    }

    public void StopAnimation(SpriteRenderer sprite)
    {
        if (_activeAnimation.ContainsKey(sprite))
        {
            _activeAnimation.Remove(sprite);
        }
    }

    public void Update()
    {
        foreach (var animation in _activeAnimation)
        {
            animation.Value.Update();
            animation.Key.sprite = animation.Value.Sprites[(int)animation.Value.Counter];
        }
    }

    public void Dispose()
    {
        _activeAnimation.Clear();
    }
}