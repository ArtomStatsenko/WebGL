using UnityEngine;

[CreateAssetMenu(fileName = "BulletModel", menuName = "Configs/BulletModel", order = 1)]
public sealed class BulletModel : ScriptableObject
{
    [SerializeField] private float _delay = 1f;
    [SerializeField] private float _startSpeed = 5f;

    public float Delay => _delay;
    public float StartSpeed => _startSpeed;
}