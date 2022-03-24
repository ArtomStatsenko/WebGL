using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public sealed class LevelGeneratorController
{
    private const int NeighbourWallCount = 4;
    private Tilemap _tileMapGround;
    private Tile _tileGround;
    private int _mapWidth;
    private int _mapHeight;
    private float _smoothFactor;
    private float _fillPercent;
    private int[,] _map;

    public LevelGeneratorController(LevelGeneratorView view)
    {
        _tileMapGround = view.TileMapGround;
        _tileGround = view.TileGround;
        _mapWidth = view.MapWidth;
        _mapHeight = view.MapHeight;
        _smoothFactor = view.SmoothFactor;
        _fillPercent = view.FillPercent;

        _map = new int[_mapWidth, _mapWidth];
    }

    public void Generate()
    {
        RandomFillMap();

        for (int i = 0; i < _smoothFactor; i++)
        {
            SmoothMap();
        }

        DrawTilesOnMap();
    }

    private void RandomFillMap()
    {

    }

    private void SmoothMap()
    {

    }

    private void DrawTilesOnMap()
    {

    }
}