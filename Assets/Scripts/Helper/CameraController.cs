using UnityEngine;

public sealed class CameraController : ILateUpdate
{
    private Transform _camera;
    private Transform _target;
    private float _speed = 2f;

    public CameraController(Transform camera, Transform target)
    {
        _camera = camera;
        _target = target;
    }

    public void LateUpdate()
    {
        float x = Mathf.Lerp(_camera.position.x, _target.position.x, Time.deltaTime * _speed);
        _camera.position = _camera.position.Change(x: x);
    }
}