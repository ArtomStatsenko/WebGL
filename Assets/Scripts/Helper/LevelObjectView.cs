using UnityEngine;
using System;

public class LevelObjectView : MonoBehaviour
{
    [SerializeField] private Collider2D _collider;
    [SerializeField] private SpriteRenderer _renderer;

    public Collider2D Collider => _collider;
    public Action<LevelObjectView> OnLevelObjectContactedEvent { get; set; }
    public SpriteRenderer Renderer => _renderer;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out LevelObjectView view))
        {
            OnLevelObjectContactedEvent?.Invoke(view);
        }
    }
}
