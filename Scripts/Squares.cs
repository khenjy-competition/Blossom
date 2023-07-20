using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum Square
{
    POT1, STICKA1, STICKB1, CORE1, PETALS1
}

[System.Serializable]
public struct SquareData
{
    public List<Tile> tile;
    public Square square;

    public List<Vector2Int> cells { get; set; }

    public void Initialize()
    {
        cells = new List<Vector2Int>();
        cells.AddRange(Data.Cells[square]);
    }
}
