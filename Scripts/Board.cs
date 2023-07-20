using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Board : MonoBehaviour
{
    private int pieceCount = 1;
    public Tilemap tilemap { get; private set; }
    public List<Piece> activePiece { get; private set; }
    public Player activePlayer { get; private set; }

    [SerializeField]
    public GameObject pieceObject;
    [SerializeField]
    public GameObject playerObject;
    [SerializeField]
    public SquareData[] Squares;
    [SerializeField]
    public Vector2Int boardSize;

    private int defaultPieceSpawnY = 20;
    private List<Vector3Int> PiecespawnPosition = new List<Vector3Int>();
    private Vector3Int PlayerspawnPosition = new Vector3Int(0, -10, -30);

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
        tilemap = GetComponentInChildren<Tilemap>();
        activePiece = new List<Piece>();
        for (int i = 0; i < pieceCount; i++)
        {
            GameObject a = Instantiate(pieceObject);
            a.name = "piece" + i;
            activePiece.Add(a.GetComponent<Piece>());
            PiecespawnPosition.Add(new Vector3Int(Random.Range(-25, 25), defaultPieceSpawnY, -21));
        }

        GameObject p = Instantiate(playerObject);
        p.name = "player";
        activePlayer = p.GetComponent<Player>();

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

        SpawnPlayer(activePlayer);
    }

    public void spawnobject(TileObject tileObject, Vector3Int spawnpos)
    {
        if (IsValidPositionObject(tileObject, spawnpos))
        {
            setObject(tileObject);
        }
    }

    public void SpawnPiece(Piece piece)
    {
        int random = Random.Range(1, Squares.Length);
        SquareData data = Squares[random];

        //Vector3Int spawnpos = new Vector3Int(Random.Range(-25, 25), 10, 0);
        Vector3Int spawnpos = new Vector3Int(0, 10, 0);
        piece.stepDelay = Random.Range(0.1f, 0.01f);
        piece.Initialize(this, spawnpos, data);

        spawnobject(piece, spawnpos);
    }

    public void SpawnPlayer(Player player)
    {
        //int random = Random.Range(0, Squares.Length);
        SquareData data = Squares[0];

        player.Initialize(this, PlayerspawnPosition, data);

        spawnobject(player, PlayerspawnPosition);
    }

    public void setObject(TileObject tileObject)
    {
        for (int i = 0; i < tileObject.data.cells.Count; i++)
        {
            Vector3Int tilePosition = tileObject.cells[i] + tileObject.position;
            tilemap.SetTile(tilePosition, tileObject.data.tile[i]);
        }
    }

    public void clearObject(TileObject tileObject)
    {
        for (int i = 0; i < tileObject.cells.Count; i++)
        {
            Vector3Int tilePosition = tileObject.cells[i] + tileObject.position;
            tilemap.SetTile(tilePosition, null);
        }
    }

    public bool isplayerExist(Piece piece)
    {
        if ((piece.position.Equals(activePlayer.position + new Vector3Int(0, 0, 0))))
        {
            return true;
        }

        return false;
    }

    public bool isRockBottom(Piece piece)
    {
        Debug.Log(piece.position.y);
        if (piece.position.y <= -boardSize.y / 2)
        {
            return true;
        }

        return false;
    }

    public bool IsValidPositionObject(TileObject tileObject, Vector3Int position)
    {
        RectInt bounds = Bounds;

        // The position is only valid if every cell is valid
        for (int i = 0; i < tileObject.cells.Count; i++)
        {
            Vector3Int tilePosition = tileObject.cells[i] + position;

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
}
