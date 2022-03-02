using UnityEngine;

[CreateAssetMenu(fileName = "PlayerModel", menuName = "Configs/PlayerModel", order = 1)]
public sealed class PlayerModel : ScriptableObject
{
    [SerializeField] private float _walkSpeed = 150f;
    [SerializeField] private float _movingTreshhold = 0.1f;
    [SerializeField] private float _jumpingTreshhold = 0.1f;
    [SerializeField] private float _jumpForce = 350f;

    public float WalkSpeed => _walkSpeed;
    public float JumpForce => _jumpForce; 
    public float MovingTreshold => _movingTreshhold;
    public float JumpingTreshold => _jumpingTreshhold;
}