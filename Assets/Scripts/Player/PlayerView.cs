using UnityEngine;

public sealed class PlayerView : LevelObjectView
{
    [SerializeField] private Rigidbody2D _rigidbody;
    public Rigidbody2D Rigidbody => _rigidbody;
}
