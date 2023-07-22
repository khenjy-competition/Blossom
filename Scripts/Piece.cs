using System.Collections.Generic;
using UnityEngine;

public class Piece : TileObject
{
    public int objectIndex { get; set; }
    public override int objectidentifier { get => 2; }
    public float stepDelay = 1f;
    private float stepTime;

    override public void Initialize(Board board, Vector3Int position, SquareData data)
    {
        base.Initialize(board, position, data);
        stepTime = Time.time + stepDelay;
    }

    private void Update()
    {
        board.clearObject(this);

        if (board.isRockBottom(this))
        {
            board.SpawnPiece(this);
        }

        // We use a timer to allow the player to make adjustments to the piece
        // before it locks in place
        //lockTime += Time.deltaTime;

        // Advance the piece to the next row every x seconds
        if (Time.time > stepTime)
        {
            Step();
        }

        board.setObject(this);
    }

    private void Step()
    {
        stepTime = Time.time + stepDelay;
        
        // Step down to the next row
        Move(Vector2Int.down);
    }

    override public Vector3Int Move(Vector2Int translation)
    {
        Vector3Int newPosition = base.Move(translation);

        bool valid = board.IsValidPositionObject(this, newPosition);

        // Only save the movement if the new position is valid
        if (valid)
        {
            position = newPosition;
            moveTime = Time.time + moveDelay;
        }

        return position;
    }
}