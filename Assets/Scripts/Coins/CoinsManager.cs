using System;
using System.Collections.Generic;
using Object = UnityEngine.Object;

public sealed class CoinsManager : IDisposable
{
    private PlayerView _playerView;
    private List<LevelObjectView> _coinViews;
    private SpriteAnimator _spriteAnimator;

    public CoinsManager(PlayerView playerView, List<LevelObjectView> coinViews, SpriteAnimator animator)
    {
        _playerView = playerView;
        _coinViews = coinViews;
        _spriteAnimator = animator;
        _playerView.OnLevelObjectContactedEvent += OnLevelObjectContacted;

        foreach (var coinView in coinViews)
        {
            _spriteAnimator.StartAnimation(coinView.SpriteRenderer, Track.CoinRotation, true);
        }
    }

    private void OnLevelObjectContacted(LevelObjectView view)
    {
        if (_coinViews.Contains(view))
        {
            _spriteAnimator.StopAnimation(view.SpriteRenderer);
            Object.Destroy(view.gameObject);
        }
    }

    public void Dispose()
    {
        _playerView.OnLevelObjectContactedEvent -= OnLevelObjectContacted;
    }
}
