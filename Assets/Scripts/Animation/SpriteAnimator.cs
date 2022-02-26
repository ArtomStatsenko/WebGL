using System;
using System.Collections.Generic;
using UnityEngine;

public sealed class SpriteAnimator : IUpdate, IDisposable
{
    private SpriteAnimationsConfig _config;
    private Dictionary<SpriteRenderer, Animation> _activeAnimation = new Dictionary<SpriteRenderer, Animation>();

    public SpriteAnimator(SpriteAnimationsConfig config)
    {
        _config = config;
    }

    public void StartAnimation(SpriteRenderer spriteRenderer, Track track, bool isLoop)
    {
        SpriteSequence sequence = _config.Sequences.Find(sequence => sequence.Track == track);
       
        if(_activeAnimation.TryGetValue(spriteRenderer, out var animation))
        {
            animation.IsLoop = isLoop;
            animation.IsSleep = false;
            animation.Speed = sequence.AnimationSpeed;

            if (animation.Track != track)
            {
                animation.Track = track;
                animation.Sprites = sequence.Sprites;
                animation.ResetCounter();
            }
        }
        else
        {
            _activeAnimation.Add(spriteRenderer, new Animation()
            {
                Track = track,
                Sprites = sequence.Sprites,
                IsLoop = isLoop,
                Speed = sequence.AnimationSpeed
            }); ;
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