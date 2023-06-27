using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Board : MonoBehaviour
{
    private int pieceCount = 5;
    public Score gscore { get; private set; }
    public Tilemap tilemap { get; private set; }
    public List<Piece> activePiece { get; private set; }
    public Player activePlayer { get; private set; }

    public SquareData[] Squares;
    public Vector2Int boardSize = new Vector2Int(30, 20);
    private List<Vector3Int> PiecespawnPosition = new List<Vector3Int>();
    private Vector3Int PlayerspawnPosition = new Vector3Int(-1, -10, 0);

    public RectInt Bounds 
    {
        get
        {
            Vector2Int position = new Vector2Int(-boardSize.x / 2, -boardSize.y / 2);
            return new RectInt(position, boardSize);
        }
    }

    private void Awake()
    {
        gscore = new Score();
        gscore.Initialize();
        tilemap = GetComponentInChildren<Tilemap>();
        activePiece = new List<Piece>();
        for (int i = 0; i < pieceCount; i++)
        {
            GameObject a = new GameObject();
            string name = "piece" + i;
            a.name = name;
            a.AddComponent(new Piece().GetType());
            //Debug.Log("a");
            activePiece.Add(a.GetComponent<Piece>());
            PiecespawnPosition.Add(new Vector3Int(Random.Range(-10, 10), 8, 0));
            activePiece[i].stepDelay = Random.Range(0.1f, 0.01f);
            activePiece[i].lockDelay = 1f;
        }

        activePlayer = GetComponentInChildren<Player>();

        for (int i = 0; i < Squares.Length; i++) {
            Squares[i].Initialize();
        }
    }

    private void Start()
    {
        foreach (Piece p in activePiece)
        {
            SpawnPiece(p);
        }

        SpawnPlayer();
    }

    public void SpawnPiece(Piece piece)
    {
        //int random = Random.Range(0, Squares.Length);
        SquareData data = Squares[1];

        Vector3Int spawnpos = new Vector3Int(Random.Range(-10, 10), 8, 0);
        piece.Initialize(this, spawnpos, data);

        if (IsValidPositionPiece(piece, spawnpos))
        {
            SetPiece(piece);
        }
    }

    public void SpawnPlayer()
    {
        //int random = Random.Range(0, Squares.Length);
        SquareData data = Squares[0];

        activePlayer.Initialize(this, PlayerspawnPosition, data);

        if (IsValidPositionPlayer(activePlayer, PlayerspawnPosition))
        {
            Debug.Log("IsValidPosition called.");
            SetPlayer(activePlayer);
        }
    }

    public void GameOver()
    {
        tilemap.ClearAllTiles();

        // Do anything else you want on game over here..
    }

    public void SetPiece(Piece piece)
    {
        for (int i = 0; i < piece.data.cells.Count; i++)
        {
            Vector3Int tilePosition = piece.cells[i] + piece.position;
            tilemap.SetTile(tilePosition, piece.data.tile[i]);
        }
    }

    public void SetPlayer(Player player)
    {
        for (int i = 0; i < player.data.cells.Count; i++)
        {
            Vector3Int tilePosition = player.cells[i] + player.position;
            tilemap.SetTile(tilePosition, player.data.tile[i]); 
        }
    }

    public void PlayerCapturePiece(Piece piece, Player player)
    {
        //Debug.Log(gscore.score.score);
        if (gscore.score >= gscore.maxScore) { return; }
        Debug.Log("PLAYER");
        if (IsPlayerExist(player, piece)) 
        {
            player.CapturePiece(player.data.cells.Count + piece.data.cells.Count, player.cells.Count + piece.cells.Count, piece);
            Debug.Log("Piece Detected");

            gscore.score += 1;
        }
        else
        {
            Debug.Log("Piece Not Detected");
        }
    }

    public void ClearPiece(Piece piece)
    {
        //Debug.Log(piece);
        for (int i = 0; i < piece.cells.Count; i++)
        {
            //Debug.Log(piece.cells[i]);
            Vector3Int tilePosition = piece.cells[i] + piece.position;
            tilemap.SetTile(tilePosition, null);
            //Debug.Log(tilePosition);
        }
        //Debug.Log("------");
    }

    public void ClearPlayer(Player player)
    {
        for (int i = 0; i < player.cells.Count; i++)
        {
            Vector3Int tilePosition = player.cells[i] + player.position;
            tilemap.SetTile(tilePosition, null);
        }
    }

    public bool IsPlayerExist(Player player, Piece piece)
    {
        Debug.Log(piece.position + " " + player.position);
        if ((piece.position.y <= player.position.y + 2))
        {
            return true;
        }

        return false;
    }

    public bool IsValidPositionPiece(Piece piece, Vector3Int position)
    {
        RectInt bounds = Bounds;

        // The position is only valid if every cell is valid
        for (int i = 0; i < piece.cells.Count; i++)
        {
            Vector3Int tilePosition = piece.cells[i] + position;

            // An out of bounds tile is invalid
            if (!bounds.Contains((Vector2Int)tilePosition)) {
                return false;
            }

            // A tile already occupies the position, thus invalid
            if (tilemap.HasTile(tilePosition)) {
                return false;
            }
        }

        return true;
    }

    public bool IsValidPositionPlayer(Player player, Vector3Int position)
    {
        RectInt bounds = Bounds;

        // The position is only valid if every cell is valid
        for (int i = 0; i < player.cells.Count; i++)
        {
            Vector3Int tilePosition = player.cells[i] + position;

            // An out of bounds tile is invalid
            if (!bounds.Contains((Vector2Int)tilePosition))
            {
                return false;
            }

            // A tile already occupies the position, thus invalid
            if (tilemap.HasTile(tilePosition))
            {
                return false;
            }
        }

        return true;
    }

    public void ClearLines()
    {
        RectInt bounds = Bounds;
        int row = bounds.yMin;

        // Clear from bottom to top
        while (row < bounds.yMax)
        {
            // Only advance to the next row if the current is not cleared
            // because the tiles above will fall down when a row is cleared
            if (IsLineFull(row)) {
                LineClear(row);
            } else {
                row++;
            }
        }
    }

    //
    public bool IsLineFull(int row)
    {
        RectInt bounds = Bounds;

        for (int col = bounds.xMin; col < bounds.xMax; col++)
        {
            Vector3Int position = new Vector3Int(col, row, 0);

            // The line is not full if a tile is missing
            if (!tilemap.HasTile(position)) {
                return false;
            }
        }

        return true;
    }


    public void LineClear(int row)
    {
        RectInt bounds = Bounds;

        // Clear all tiles in the row
        for (int col = bounds.xMin; col < bounds.xMax; col++)
        {
            Vector3Int position = new Vector3Int(col, row, 0);
            tilemap.SetTile(position, null);
        }

        // Shift every row above down one
        while (row < bounds.yMax)
        {
            for (int col = bounds.xMin; col < bounds.xMax; col++)
            {
                Vector3Int position = new Vector3Int(col, row + 1, 0);
                TileBase above = tilemap.GetTile(position);

                position = new Vector3Int(col, row, 0);
                tilemap.SetTile(position, above);
            }

            row++;
        }
    }

}
