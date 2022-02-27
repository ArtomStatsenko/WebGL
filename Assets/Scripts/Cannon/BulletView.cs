using UnityEngine;

public sealed class BulletView : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;

    public void SetVisible(bool isVisible)
    {
        _spriteRenderer.enabled = isVisible;
    }
}