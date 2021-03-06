using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public sealed class SpriteSequence
{
    public Track Track;
    public float AnimationSpeed = 12f;
    public List<Sprite> Sprites = new List<Sprite>();
}
