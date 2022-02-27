using System.Collections.Generic;
using UnityEngine;

public sealed class BulletsEmitter : IUpdate
{
    private BulletModel _model;
    private Transform _transform;
    private int _currentIndex;
    private float _timeTillNextBullet;
    private GameObjectPool _bulletsPool;
    private List<BulletController> _bullets = new List<BulletController>();

    public BulletsEmitter(BulletModel model, GameObject prefab, Transform transfrom)
    {
        _model = model;
        _transform = transfrom;
        _bulletsPool = new GameObjectPool(prefab);
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

            GameObject go = _bulletsPool.GetGameObject();
            BulletView view = go.GetComponent<BulletView>();
            _bullets.Add(new BulletController(view));

            _bullets[_currentIndex].Throw(_transform.position, _transform.up * _model.StartSpeed);
            _currentIndex++;

            if (_currentIndex >= _bullets.Count)
            {
                _currentIndex = 0;
            }
        }

        _bullets.ForEach(b => b.Update());
    }
}
