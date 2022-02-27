using UnityEngine;

public sealed class CannonView : MonoBehaviour
{
    [SerializeField] Transform _muzzle;

    public Transform Muzzle => _muzzle;
}
