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
    private int _smoothFactor;
    private int _fillPercent;
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

        for (var i = 0; i < _smoothFactor; i++)
        {
            SmoothMap();
        }

        DrawTilesOnMap();
    }

    public void Clear()
    {
        if (_tileMapGround != null)
        {
            _tileMapGround.ClearAllTiles();
        }
    }

    private void RandomFillMap()
    {
        for (var x = 0; x < _mapWidth; x++)
        {
            for (var y = 0; y < _mapHeight; y++)
            {
                if (x == 0 || x == _mapWidth - 1 || y == 0 || y == _mapHeight - 1)
                {
                    _map[x, y] = 1;
                }
                else
                {
                    _map[x, y] = (UnityEngine.Random.Range(0, 101) < _fillPercent) ? 1 : 0;
                }
            }
        }
    }
    private void SmoothMap()
    {
        for (var x = 0; x < _mapWidth; x++)
        {
            for (var y = 0; y < _mapHeight; y++)
            {
                var neighbourWallTiles = GetSurroundingWallCount(x, y);

                if (neighbourWallTiles > NeighbourWallCount)
                {
                    _map[x, y] = 1;
                }
                else if (neighbourWallTiles < NeighbourWallCount)
                {
                    _map[x, y] = 0;
                }
            }
        }
    }

    private int GetSurroundingWallCount(int x, int y)
    {
        var wallCount = 0;

        for (var neighbourX = x - 1; neighbourX <= x + 1; neighbourX++)
        {
            for (var neighbourY = y - 1; neighbourY <= y + 1; neighbourY++)
            {
                if (neighbourX >= 0 && neighbourX < _mapWidth && neighbourY >= 0 && neighbourY < _mapHeight)
                {
                    if (neighbourX != x || neighbourY != y)
                    {
                        wallCount += _map[neighbourX, neighbourY];
                    }
                }
                else
                {
                    wallCount++;
                }
            }
        }

        return wallCount;
    }

    private void DrawTilesOnMap()
    {
        if (_map == null)
        {
            return;
        }

        for (var x = 0; x < _mapWidth; x++)
        {
            for (var y = 0; y < _mapHeight; y++)
            {
                var positionTile = new Vector3Int(-_mapWidth / 2 + x, -_mapHeight / 2 + y, 0);

                if (_map[x, y] == 1)
                {
                    _tileMapGround.SetTile(positionTile, _tileGround);
                }
            }
        }
    }
}