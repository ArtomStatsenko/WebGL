using UnityEngine;

[CreateAssetMenu(fileName = "PlayerModel", menuName = "Configs/PlayerModel", order = 1)]
public sealed class PlayerModel : ScriptableObject
{
    [SerializeField] private float _walkSpeed = 1f;
    [SerializeField] private float _movingTreshhold = 0.1f;
    [SerializeField] private float _jumpingTreshhold = 1f;
    [SerializeField] private float _startJumpSpeed = 3f;
    [SerializeField] private float _mass = 2f;

    public float WalkSpeed => _walkSpeed;
    public float MovingTreshold => _movingTreshhold;
    public float JumpingTreshold => _jumpingTreshhold;
    public float StartJumpSpeed => _startJumpSpeed; 
    public float Mass => _mass;

}