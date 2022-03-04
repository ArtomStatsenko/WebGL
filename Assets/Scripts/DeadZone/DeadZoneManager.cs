using System;
using UnityEngine;

public sealed class DeadZoneManager : IDisposable
{
    private PlayerView _playerView;
    private Vector3 _playerStartPosition;

    public DeadZoneManager(PlayerView playerView)
    {
        _playerView = playerView;
        _playerView.OnLevelObjectContactedEvent += OnLevelObjectContacted;
        _playerStartPosition = playerView.transform.position;
    }

    private void OnLevelObjectContacted(LevelObjectView view)
    {
        var deadZoneView = view as DeadZoneView;
        if (deadZoneView != null)
        {
            _playerView.transform.position = _playerStartPosition;
        }
    }

    public void Dispose()
    {
        _playerView.OnLevelObjectContactedEvent -= OnLevelObjectContacted;
    }
}
