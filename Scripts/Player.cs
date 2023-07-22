using System.Collections.Generic;
using UnityEngine;

public class Player : TileObject
{
    public override int objectidentifier { get => 1; }

    public void AssimilatePiece(int tetrominonewCount, int newCount, Piece piece)
    {
        data.tile.AddRange(piece.data.tile);

        Debug.Log(newCount);
        for (int i = this.cells.Count; i < newCount; i++)
        {
            cells.Add(cells[i - piece.cells.Count] + new Vector3Int(0, 1, 0));
        }
    }

    private void Update()
    {
        board.clearObject(this);

        // Allow the player to hold movement keys but only after a move delay
        // so it does not move too fast
        if (Time.time > moveTime)
        {
            HandleMoveInputs();
        }

        board.setObject(this);
    }

    private void HandleMoveInputs()
    {
        // Left/right movement
        if (Input.GetKey(KeyCode.A))
        {
            Move(Vector2Int.left);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            Move(Vector2Int.right);
        }
    }

    override public Vector3Int Move(Vector2Int translation)
    {
        Vector3Int newPosition = base.Move(translation);

        bool valid = board.IsValidPositionObject(this, newPosition);

        // Only save the movement if the new position is valid
        if (valid)
        {
            position = newPosition;
        }

        return position;
    }
}
