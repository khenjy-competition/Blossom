using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileObject : MonoBehaviour
{
    virtual public int objectidentifier { get; private set; }
    public Board board { get; private set; }
    public Tilemap tilemap { get; private set; }
    public SquareData data { get; set; }
    public List<Vector3Int> cells { get; set; }
    public Vector3Int position { get; set; }

    virtual public float moveDelay { get; }
    public float moveTime { get; set; }

    virtual public void Initialize(Board board, Vector3Int position, SquareData data)
    {
        this.data = data;
        this.board = board;
        this.position = position;
        this.tilemap = board.tilemap;

        moveTime = Time.time + moveDelay;

        if (cells == null)
        {
            cells = new List<Vector3Int>();

            Vector3Int[] temp = new Vector3Int[data.cells.Count];
            for (int i = 0; i < data.cells.Count; i++)
            {
                temp[i] = (Vector3Int)data.cells[i];
            }

            cells.AddRange(temp);
        }
    }

    virtual public Vector3Int Move(Vector2Int translation)
    {
        Vector3Int newPosition = position;
        newPosition.x += translation.x;
        newPosition.y += translation.y;

        return newPosition;
    }
}