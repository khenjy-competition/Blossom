using System.Collections.Generic;
using UnityEngine;

public static class Data
{
    public static readonly float cos = Mathf.Cos(Mathf.PI / 2f);
    public static readonly float sin = Mathf.Sin(Mathf.PI / 2f);
    public static readonly float[] RotationMatrix = new float[] { cos, sin, -sin, cos };

    public static readonly Dictionary<Square, Vector2Int[]> Cells = new Dictionary<Square, Vector2Int[]>()
    {
        { Square.A, new Vector2Int[] { 
            new Vector2Int(-1, 0), new Vector2Int( 0, 0), new Vector2Int( 1, 0), new Vector2Int( 2, 0), 
            new Vector2Int(-1, 1), new Vector2Int( 0, 1), new Vector2Int( 1, 1), new Vector2Int( 2, 1), 
            new Vector2Int(-1, 2), new Vector2Int( 0, 2), new Vector2Int( 1, 2), new Vector2Int( 2, 2) } 
        },
        { Square.B, new Vector2Int[] { new Vector2Int(-1, 1), new Vector2Int(0, 1), new Vector2Int(1, 1) } },
        { Square.C, new Vector2Int[] { new Vector2Int(-1, 2) } },
        { Square.D, new Vector2Int[] { new Vector2Int( 0, 0) } },
        { Square.E, new Vector2Int[] { new Vector2Int( 0, 1) } },
        { Square.F, new Vector2Int[] { new Vector2Int( 0, 2) } },
        { Square.G, new Vector2Int[] { new Vector2Int( 1, 0) } },
        { Square.H, new Vector2Int[] { new Vector2Int( 1, 1) } },
        { Square.I, new Vector2Int[] { new Vector2Int( 1, 2) } },
        { Square.J, new Vector2Int[] { new Vector2Int( 2, 0) } },
        { Square.K, new Vector2Int[] { new Vector2Int( 2, 1) } },
        { Square.L, new Vector2Int[] { new Vector2Int( 2, 2) } },
    };

    private static readonly Vector2Int[,] WallKicksI = new Vector2Int[,] {
        { new Vector2Int(0, 0) },
        { new Vector2Int(0, 0) },
        { new Vector2Int(0, 0) },
        { new Vector2Int(0, 0) },
        { new Vector2Int(0, 0) },
        { new Vector2Int(0, 0) },
        { new Vector2Int(0, 0) },
        { new Vector2Int(0, 0) },
        { new Vector2Int(0, 0) },
        { new Vector2Int(0, 0) },
        { new Vector2Int(0, 0) },
        { new Vector2Int(0, 0) },
    };

    private static readonly Vector2Int[,] WallKicksJLOSTZ = new Vector2Int[,] {
        { new Vector2Int(0, 0) },
        { new Vector2Int(0, 0) },
        { new Vector2Int(0, 0) },
        { new Vector2Int(0, 0) },
        { new Vector2Int(0, 0) },
        { new Vector2Int(0, 0) },
        { new Vector2Int(0, 0) },
        { new Vector2Int(0, 0) },
        { new Vector2Int(0, 0) },
        { new Vector2Int(0, 0) },
        { new Vector2Int(0, 0) },
        { new Vector2Int(0, 0) },
    };

    /*private static readonly Vector2Int[,] WallKicksI = new Vector2Int[,] {
        { new Vector2Int(0, 0), new Vector2Int(-2, 0), new Vector2Int( 1, 0), new Vector2Int(-2,-1), new Vector2Int( 1, 2) },
        { new Vector2Int(0, 0), new Vector2Int( 2, 0), new Vector2Int(-1, 0), new Vector2Int( 2, 1), new Vector2Int(-1,-2) },
        { new Vector2Int(0, 0), new Vector2Int(-1, 0), new Vector2Int( 2, 0), new Vector2Int(-1, 2), new Vector2Int( 2,-1) },
        { new Vector2Int(0, 0), new Vector2Int( 1, 0), new Vector2Int(-2, 0), new Vector2Int( 1,-2), new Vector2Int(-2, 1) },
        { new Vector2Int(0, 0), new Vector2Int( 2, 0), new Vector2Int(-1, 0), new Vector2Int( 2, 1), new Vector2Int(-1,-2) },
        { new Vector2Int(0, 0), new Vector2Int(-2, 0), new Vector2Int( 1, 0), new Vector2Int(-2,-1), new Vector2Int( 1, 2) },
        { new Vector2Int(0, 0), new Vector2Int( 1, 0), new Vector2Int(-2, 0), new Vector2Int( 1,-2), new Vector2Int(-2, 1) },
        { new Vector2Int(0, 0), new Vector2Int(-1, 0), new Vector2Int( 2, 0), new Vector2Int(-1, 2), new Vector2Int( 2,-1) },
    };

    private static readonly Vector2Int[,] WallKicksJLOSTZ = new Vector2Int[,] {
        { new Vector2Int(0, 0), new Vector2Int(-1, 0), new Vector2Int(-1, 1), new Vector2Int(0,-2), new Vector2Int(-1,-2) },
        { new Vector2Int(0, 0), new Vector2Int( 1, 0), new Vector2Int( 1,-1), new Vector2Int(0, 2), new Vector2Int( 1, 2) },
        { new Vector2Int(0, 0), new Vector2Int( 1, 0), new Vector2Int( 1,-1), new Vector2Int(0, 2), new Vector2Int( 1, 2) },
        { new Vector2Int(0, 0), new Vector2Int(-1, 0), new Vector2Int(-1, 1), new Vector2Int(0,-2), new Vector2Int(-1,-2) },
        { new Vector2Int(0, 0), new Vector2Int( 1, 0), new Vector2Int( 1, 1), new Vector2Int(0,-2), new Vector2Int( 1,-2) },
        { new Vector2Int(0, 0), new Vector2Int(-1, 0), new Vector2Int(-1,-1), new Vector2Int(0, 2), new Vector2Int(-1, 2) },
        { new Vector2Int(0, 0), new Vector2Int(-1, 0), new Vector2Int(-1,-1), new Vector2Int(0, 2), new Vector2Int(-1, 2) },
        { new Vector2Int(0, 0), new Vector2Int( 1, 0), new Vector2Int( 1, 1), new Vector2Int(0,-2), new Vector2Int( 1,-2) },
    };*/

    public static readonly Dictionary<Square, Vector2Int[,]> WallKicks = new Dictionary<Square, Vector2Int[,]>()
    {
        { Square.A, WallKicksI },
        { Square.B, WallKicksI },
        { Square.C, WallKicksI },
        { Square.D, WallKicksI },
        { Square.E, WallKicksI },
        { Square.F, WallKicksI },
        { Square.G, WallKicksI },
        { Square.H, WallKicksI },
        { Square.I, WallKicksI },
        { Square.J, WallKicksI },
        { Square.K, WallKicksI },
        { Square.L, WallKicksI },
    };

}
