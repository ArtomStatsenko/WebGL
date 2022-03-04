using UnityEngine;

public sealed class CoinView : LevelObjectView
{
    [SerializeField] private SpriteRenderer _spriteRenderer;

    public SpriteRenderer SpriteRenderer => _spriteRenderer;
}