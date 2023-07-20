using System.Collections.Generic;
using UnityEngine;

public static class Data
{
    public static readonly float cos = Mathf.Cos(Mathf.PI / 2f);
    public static readonly float sin = Mathf.Sin(Mathf.PI / 2f);
    public static readonly float[] RotationMatrix = new float[] { cos, sin, -sin, cos };

    public static readonly Dictionary<Square, Vector2Int[]> Cells = new Dictionary<Square, Vector2Int[]>()
    {
        { Square.POT1, new Vector2Int[] {
            new Vector2Int(-1, 0) }
        },
        { Square.STICKA1, new Vector2Int[] {
            new Vector2Int(-1, 0)}
        },
        { Square.STICKB1, new Vector2Int[] {
            new Vector2Int(-1, 0) }
        },
        { Square.CORE1, new Vector2Int[] {
            new Vector2Int(-1, 0) }
        },
        { Square.PETALS1, new Vector2Int[] {
            new Vector2Int(-1, 0) }
        }
    };
}
