using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    public int index { get; private set; }
    public Board board { get; private set; }
    public SquareData data { get; private set; }
    public List<Vector3Int> cells { get; private set; }
    public Vector3Int position { get; private set; }
    public int rotationIndex { get; private set; }

    public float stepDelay = 1f;
    public float moveDelay = 0.1f;
    public float lockDelay = 0.5f;

    private float stepTime;
    private float moveTime;
    private float lockTime;

    //posisi pertama
    public void Initialize(Board board, Vector3Int position, SquareData data)
    { 
        this.data = data;
        this.board = board;
        this.position = position;

        rotationIndex = 0;
        stepTime = Time.time + stepDelay;
        moveTime = Time.time + moveDelay;
        lockTime = 0f;

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

    //keyboard input 2
    private void Update()
    {
        board.ClearPiece(this);

        // We use a timer to allow the player to make adjustments to the piece
        // before it locks in place
        //lockTime += Time.deltaTime;

        // Advance the piece to the next row every x seconds
        if (Time.time > stepTime)
        {
            Step();
        }

        board.SetPiece(this);
    }

    private void Step()
    {
        stepTime = Time.time + stepDelay;
        
        // Step down to the next row
        Move(Vector2Int.down);
    }

    //gerakin
    private bool Move(Vector2Int translation)
    {
        Vector3Int newPosition = position;
        newPosition.x += translation.x;
        newPosition.y += translation.y;

        bool valid = board.IsValidPositionPiece(this, newPosition);
        bool pexist = board.IsPlayerExist(board.activePlayer, this);

        // Only save the movement if the new position is valid
        if (pexist)
        {
            board.PlayerCapturePiece(this, board.activePlayer);
            board.SpawnPiece(this);
        }
        else
        {
            position = newPosition;
            moveTime = Time.time + moveDelay;
            lockTime = 0f; // reset
        }

        return valid;
    }
}
