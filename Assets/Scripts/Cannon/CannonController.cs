using UnityEngine;

public sealed class CannonController : IUpdate
{
    private Transform _muzzle;
    private Transform _aim;

    public CannonController(Transform muzzle, Transform aim)
    {
        _muzzle = muzzle;
        _aim = aim;
    }

    public void Update()
    {
        Vector3 direction = _aim.position - _muzzle.position;
        float angle = Vector3.Angle(Vector3.down, direction);
        Vector3 axis = Vector3.Cross(Vector3.down, direction);
        _muzzle.rotation = Quaternion.AngleAxis(angle, axis);
    }
}