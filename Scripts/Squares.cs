using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum Square
{
    A, B, C, D, E, F, G, H, I, J, K, L
}

[System.Serializable]
public struct SquareData
{
    public List<Tile> tile;
    public Square square;

    public List<Vector2Int> cells { get; set; }
    public List<Vector2Int[,]> wallKicks { get; private set; }

    public void Initialize()
    {
        cells = new List<Vector2Int>();
        wallKicks = new List<Vector2Int[,]>();
        cells.AddRange(Data.Cells[square]);
        wallKicks.Add(Data.WallKicks[square]);
    }
}
