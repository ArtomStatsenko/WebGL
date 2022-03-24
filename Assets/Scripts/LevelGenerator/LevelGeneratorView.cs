using UnityEngine;
using UnityEngine.Tilemaps;

public sealed class LevelGeneratorView : MonoBehaviour
{
    [SerializeField] private Tilemap _tileMapGround;
    [SerializeField] private Tile _tileGround;
    [SerializeField] private int _mapWidth;
    [SerializeField] private int _mapHeight;
    [SerializeField] private float _smoothFactor;
    [SerializeField] [Range(0f, 100f)] private float _fillPercent;

    public Tilemap TileMapGround => _tileMapGround;
    public Tile TileGround => _tileGround;
    public int MapWidth => _mapWidth;
    public int MapHeight => _mapHeight;
    public float SmoothFactor => _smoothFactor;
    public float FillPercent => _fillPercent;
}
