using UnityEngine;
using System;

public class LevelObjectView : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Collider2D _collider;

    public SpriteRenderer SpriteRenderer => _spriteRenderer;
    public Collider2D Collider => _collider;
    public Action<LevelObjectView> OnLevelObjectContactedEvent { get; set; }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out LevelObjectView view))
        {
            OnLevelObjectContactedEvent?.Invoke(view);
        }
    }
}
