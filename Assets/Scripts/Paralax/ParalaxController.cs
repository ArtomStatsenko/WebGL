using UnityEngine;

public sealed class ParalaxController : IUpdate
{
    private Transform _camera;
    private ParalaxBackground[] _backgrounds;
    private Vector3[] _backgroundsStartPosition;
    private Vector3 _cameraStartPosition;

    public ParalaxController(Transform camera, ParalaxBackground[] backgrounds)
    {
        _camera = camera;
        _backgrounds = backgrounds;
        _cameraStartPosition = _camera.transform.position;
        _backgroundsStartPosition = new Vector3[backgrounds.Length];

        for (int i = 0; i < backgrounds.Length; i++)
        {
            _backgroundsStartPosition[i] = backgrounds[i].Transform.position;
        }
    }

    public void Update()
    {
        Vector3 direction = _camera.position - _cameraStartPosition;
        for (int i = 0; i < _backgrounds.Length; i++)
        {
            _backgrounds[i].Transform.position = _backgroundsStartPosition[i] + direction * _backgrounds[i].SpeedCoef;
        }
    }
}
