using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpriteAnimationConfig", menuName = "Configs/SpriteAnimationConfig", order = 1)]
public sealed class SpriteAnimationsConfig : ScriptableObject
{
    public List<SpriteSequence> Sequences = new List<SpriteSequence>();
}