using UnityEngine;

public sealed class BackgroundLayerController : MonoBehaviour
{
    [SerializeField] private Transform _left;
    [SerializeField] private Transform _right;
    [SerializeField] private float _movingOffsetX = 9.28f;

    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        if (_camera.transform.position.x > _right.transform.position.x)
        {
            _left.transform.position = _left.transform.position.Change(x: _left.transform.position.x + (_movingOffsetX * 2f));

            Transform temp = _left;
            _left = _right;
            _right = temp;
        }

        if (_camera.transform.position.x < _left.transform.position.x)
        {
            _right.transform.position = _right.transform.position.Change(x: _right.transform.position.x - (_movingOffsetX * 2f));

            Transform temp = _right;
            _right = _left;
            _left = temp;
        }
    }
}
