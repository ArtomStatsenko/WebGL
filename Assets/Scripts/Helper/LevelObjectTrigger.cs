using System;
using UnityEngine;

public sealed class LevelObjectTrigger : MonoBehaviour
{
    public event EventHandler<GameObject> OnTriggerEnterEvent;
    public event EventHandler<GameObject> OnTriggerExitEvent;

    private void OnTriggerEnter2D(Collider2D other)
    {
        OnTriggerEnterEvent?.Invoke(this, other.gameObject);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        OnTriggerExitEvent?.Invoke(this, other.gameObject);
    }
}
