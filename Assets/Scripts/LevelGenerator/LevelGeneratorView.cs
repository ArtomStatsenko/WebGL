using UnityEngine;
using UnityEngine.Tilemaps;

public sealed class LevelGeneratorView : MonoBehaviour
{
    [SerializeField] private Tilemap _tileMapGround;
    [SerializeField] private Tile _tileGround;
    [SerializeField] private int _mapWidth;
    [SerializeField] private int _mapHeight;
    [SerializeField] private int _smoothFactor;
    [SerializeField] [Range(0, 100)] private int _fillPercent;

    public Tilemap TileMapGround => _tileMapGround;
    public Tile TileGround => _tileGround;
    public int MapWidth => _mapWidth;
    public int MapHeight => _mapHeight;
    public int SmoothFactor => _smoothFactor;
    public int FillPercent => _fillPercent;
}
