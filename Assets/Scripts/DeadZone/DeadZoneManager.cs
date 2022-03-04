using System;
using System.Collections.Generic;
using UnityEngine;

public sealed class DeadZoneManager : IDisposable
{
    private PlayerView _playerView;
    private List<LevelObjectView> _deadZoneViews;
    private Vector3 _playerStartPosition;

    public DeadZoneManager(PlayerView playerView, List<LevelObjectView> deadZoneViews)
    {
        _playerView = playerView;
        _deadZoneViews = deadZoneViews;
        _playerView.OnLevelObjectContactedEvent += OnLevelObjectContacted;
        _playerStartPosition = playerView.transform.position;
    }

    private void OnLevelObjectContacted(LevelObjectView view)
    {
        if (_deadZoneViews.Contains(view))
        {
            _playerView.transform.position = _playerStartPosition;
        }
    }

    public void Dispose()
    {
        _playerView.OnLevelObjectContactedEvent -= OnLevelObjectContacted;
    }
}
