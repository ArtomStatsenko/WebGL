using System;
using Object = UnityEngine.Object;

public sealed class CoinsManager : IDisposable
{
    private PlayerView _playerView;
    private SpriteAnimator _spriteAnimator;

    public CoinsManager(PlayerView playerView, CoinView[] coinViews, SpriteAnimator animator)
    {
        _playerView = playerView;
        _spriteAnimator = animator;
        _playerView.OnLevelObjectContactedEvent += OnLevelObjectContacted;

        foreach (var coinView in coinViews)
        {
            _spriteAnimator.StartAnimation(coinView.SpriteRenderer, Track.CoinRotation, true);
        }
    }

    private void OnLevelObjectContacted(LevelObjectView view)
    {
        var coinView = view as CoinView;
        if (coinView != null)
        {
            _spriteAnimator.StopAnimation(coinView.SpriteRenderer);
            Object.Destroy(coinView.gameObject);
        }
    }

    public void Dispose()
    {
        _playerView.OnLevelObjectContactedEvent -= OnLevelObjectContacted;
    }
}
