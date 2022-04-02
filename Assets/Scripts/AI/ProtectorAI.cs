using Pathfinding;
using System;
using UnityEngine;

public sealed class ProtectorAI : IProtector
{
    private readonly EnemyView _view;
    private readonly AIDestinationSetter _destinationSetter;
    private readonly AIPatrolPath _patrolPath;
    private int _currentPointIndex;
    private readonly Transform[] _waypoints;
    private bool _isPatrolling;

    public ProtectorAI(EnemyView view, Transform[] waypoints, AIDestinationSetter destinationSetter, AIPatrolPath patrolPath, SpriteAnimator animator)
    {
        _view = view;
        _waypoints = waypoints;
        _destinationSetter = destinationSetter;
        _patrolPath = patrolPath;

        animator.StartAnimation(view.SpriteRenderer, Track.JinnIdle, true);
    }

    public void Init()
    {
        _destinationSetter.target = GetNextTarget();
        _isPatrolling = true;
        _patrolPath.TargetReached += OnTargetReached;
    }

    public void Dispose()
    {
        _patrolPath.TargetReached -= OnTargetReached;
    }

    private Transform GetNextTarget()
    {
        if (_waypoints == null)
        {
            return null;
        }

        _currentPointIndex = (_currentPointIndex + 1) % _waypoints.Length;
        return _waypoints[_currentPointIndex];
    }

    public Transform GetClosestTarget(Vector2 fromPosition)
    {
        if (_waypoints == null)
        {
            return null;
        }

        var closestIndex = 0;
        var closestDistance = 0.0f;
        for (var i = 0; i < _waypoints.Length; i++)
        {
            var distance = Vector2.Distance(fromPosition, _waypoints[i].position);
            if (closestDistance > distance)
            {
                closestDistance = distance;
                closestIndex = i;
            }
        }
        _currentPointIndex = closestIndex;

        return _waypoints[_currentPointIndex];
    }

    private void OnTargetReached(object sender, EventArgs e)
    {
        _destinationSetter.target = _isPatrolling ? GetNextTarget() : GetClosestTarget(_view.transform.position);
    }

    public void StartProtection(GameObject invader)
    {
        _isPatrolling = false;
        _destinationSetter.target = invader.transform;
    }

    public void FinishProtection(GameObject invader)
    {
        _isPatrolling = true;
        _destinationSetter.target = GetClosestTarget(_view.transform.position);
    }
}
