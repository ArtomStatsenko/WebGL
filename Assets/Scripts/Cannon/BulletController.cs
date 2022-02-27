using UnityEngine;

public sealed class BulletController : IUpdate
{
    private float _groundLevel = 0f;
    private float _g = -9.8f;
    private Vector3 _velocity;
    private BulletView _view;

    public BulletController(BulletView view)
    {
        _view = view;
    }

    public void Update()
    {
        if (IsGrounded())
        {
            SetVelocity(_velocity.Change(y: -_velocity.y));
            _view.transform.position = _view.transform.position.Change(y: _groundLevel);
        }
        else
        {
            SetVelocity(_velocity + Vector3.up * _g * Time.deltaTime);
            _view.transform.position += _velocity * Time.deltaTime;
        }
    }

    private bool IsGrounded()
    {
        return _view.transform.position.y <= _groundLevel + float.Epsilon && _velocity.y <= 0f;
    }

    public void Throw(Vector3 position, Vector3 velocity)
    {
        _view.transform.position = position;
        SetVelocity(velocity);
        _view.SetVisible(true);
    }

    private void SetVelocity(Vector3 velocity)
    {
        _velocity = velocity;
        var angle = Vector3.Angle(Vector3.left, _velocity);
        var axis = Vector3.Cross(Vector3.left, _velocity);
        _view.transform.rotation = Quaternion.AngleAxis(angle, axis);
    }
}