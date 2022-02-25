using UnityEngine;

public sealed class PlayerView : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;

    public SpriteRenderer SpriteRenderer => _spriteRenderer;
}
