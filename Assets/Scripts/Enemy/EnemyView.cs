using UnityEngine;

public sealed class EnemyView : LevelObjectView
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    public Rigidbody2D Rigidbody => _rigidbody;
    public SpriteRenderer SpriteRenderer => _spriteRenderer;
}