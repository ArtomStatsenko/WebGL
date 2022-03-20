using System;
using UnityEngine;

[Serializable]
public struct AIConfig
{
    public float Speed;
    public float MinDistance;
    public Transform[] Waypoints;
}