using System.Collections.Generic;
using UnityEngine;

public sealed class BulletsEmitter : IUpdate
{
    private BulletModel _model;
    private Transform _transform;
    private int _currentIndex;
    private int _bulletQuantity = 5;
    private float _timeTillNextBullet;
    private List<BulletController> _bullets = new List<BulletController>();

    public BulletsEmitter(BulletModel model, GameObject bulletPrefab, Transform transform)
    {
        _model = model;
        _transform = transform;

        for (int i = 0; i < _bulletQuantity; i++)
        {
            GameObject bullet = Object.Instantiate(bulletPrefab);
            BulletView view = bullet.GetComponent<BulletView>();
            _bullets.Add(new BulletController(view));
        }
    }

    public void Update()
    {
        if (_timeTillNextBullet > 0f)
        {
            _timeTillNextBullet -= Time.deltaTime;
        }
        else
        {
            _timeTillNextBullet = _model.Delay;
            _bullets[_currentIndex].Throw(_transform.position, -_transform.up * _model.StartSpeed);
            _currentIndex++;
            if (_currentIndex >= _bullets.Count)
            {
                _currentIndex = 0;
            }
        }

        _bullets.ForEach(b => b.Update());
    }
}
