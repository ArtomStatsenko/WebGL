using UnityEngine;

public sealed class BackgroundLayerView : MonoBehaviour
{
    [SerializeField] private float _speedCoef;
    [SerializeField] private float _movingOffsetX = 18.56f;
    [SerializeField] private Transform _left;
    [SerializeField] private Transform _right;

    public float SpeedCoef => _speedCoef;
    public float MovingOffsetX => _movingOffsetX;
    public Transform Left => _left;
    public Transform Right => _right;

    public void SwapTransforms()
    {
        Transform temp = _left;
        _left = _right;
        _right = temp;
    }
}
