using Pathfinding;
using UnityEngine;

public class StalkerAI : MonoBehaviour
{
    private readonly EnemyView _view;
    private readonly Seeker _seeker;
    private readonly Transform _target;
    private readonly AIConfig _config;
    private Path _path;
    private int _currentPointIndex;

    public StalkerAI(EnemyView view, Seeker seeker, Transform target, AIConfig config, SpriteAnimator animator)
    {
        _view = view;
        _seeker = seeker;
        _target = target;
        _config = config;

        animator.StartAnimation(view.SpriteRenderer, Track.DragonIdle, true);
    }

    public void FixedUpdate()
    {
        var newVelocity = CalculateVelocity(_view.transform.position);
        _view.Rigidbody.velocity = newVelocity;

        _view.SpriteRenderer.flipX = newVelocity.x < 0f;
    }

    public Vector2 CalculateVelocity(Vector2 fromPosition)
    {
        if (_path == null)
        {
            return Vector2.zero;
        }

        if (_currentPointIndex >= _path.vectorPath.Count)
        {
            return Vector2.zero;
        }

        var direction = ((Vector2)_path.vectorPath[_currentPointIndex] - fromPosition).normalized;
        var result = _config.Speed * direction;
        var distance = Vector2.SqrMagnitude((Vector2)_path.vectorPath[_currentPointIndex] - fromPosition);

        if (distance <= _config.MinDistance)
        {
            _currentPointIndex++;
        }

        return result;
    }

    public void RecalculatePath()
    {
        if (_seeker.IsDone())
        {
            _seeker.StartPath(_view.Rigidbody.position, _target.position, OnPathComplete);
        }
    }

    private void OnPathComplete(Path path)
    {
        if (path.error)
        {
            return;
        }

        UpdatePath(path);
    }

    public void UpdatePath(Path path)
    {
        _path = path;
        _currentPointIndex = 0;
    }   
}