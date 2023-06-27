using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Board board { get; private set; }
    public SquareData data { get; private set; }
    public List<Vector3Int> cells { get; private set; }
    public Vector3Int position { get; private set; }
    public int rotationIndex { get; private set; }

    public float moveDelay = 0.1f;

    private float moveTime;

    public void Initialize(Board board, Vector3Int position, SquareData data)
    {
        this.data = data;
        this.board = board;
        this.position = position;

        rotationIndex = 0;
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

    public void CapturePiece(int tetrominonewCount, int newCount, Piece piece)
    {
        //Vector3Int[] oldcells = cells;
        data.tile.AddRange(piece.data.tile);

        /*for (int i = 0; i < cells.Count; i++)
        {
            Debug.Log(cells[i]);
        }*/
        //Debug.Log("Before");

        List<Vector3Int> temp = new List<Vector3Int>();
        temp.AddRange(cells);
        Debug.Log(newCount);
        for (int i = temp.Count; i <= newCount; i++)
        {
            cells.Add(cells[i - 1 - piece.cells.Count] + new Vector3Int(0, 1, 0));
            //Debug.Log(cells[i - temp.Count] + new Vector3Int(0, 1, 0));
        }

        //board.gscore.score.score -= 1;

        /*for (int i = 0; i < cells.Count; i++)
        {
            //Debug.Log(i + " " + cells[i]);
        }*/
        //Debug.Log("After");
    }

    //keyboard input 2
    private void Update()
    {
        board.ClearPlayer(this);

        // Allow the player to hold movement keys but only after a move delay
        // so it does not move too fast
        if (Time.time > moveTime)
        {
            HandleMoveInputs();
        }

        board.SetPlayer(this);
    }

    //keyboard input
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

    //gerakin
    private bool Move(Vector2Int translation)
    {
        Vector3Int newPosition = position;
        newPosition.x += translation.x;
        newPosition.y += translation.y;

        bool valid = board.IsValidPositionPlayer(this, newPosition);

        // Only save the movement if the new position is valid
        if (valid)
        {
            position = newPosition;
            moveTime = Time.time + moveDelay;
            //lockTime = 0f; // reset
        }

        return valid;
    }
}
