using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Board : MonoBehaviour
{
    public Tilemap tilemap { get; set; }
    public TetraminoTile[] tetraminos;
    public Piece activePiece { get; set; }
    public Vector3Int spawnPosition = new Vector3Int (4, 0, 0);

    public static readonly int height = 20;
    public static readonly int width = 10;
    public int leftBorder = 0;
    public int rightBorder = 9;
    public int upperBorder = 19;
    public int lowerBorder = 0;
    public int[,] currentBoard = new int[ width, height ];


    private void Awake()
    {
        this.tilemap = GetComponentInChildren<Tilemap>();
        this.activePiece = GetComponentInChildren<Piece>();

        for (int i = 0; i < this.tetraminos.Length; i++)
        {
            this.tetraminos[i].Initialize();
        }
        
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
                currentBoard[i, j] = 0;
        }
    }

    private void Start()
    {
        Spawn();
    }

    public void Spawn()
    {
        int rand = Random.Range(0, this.tetraminos.Length);
        TetraminoTile newTile = this.tetraminos[rand];
        this.activePiece.Initialize(this, this.spawnPosition, newTile);
        this.activePiece.tetraNum = rand;
        Set(this.activePiece);
    }

    public void Set(Piece piece)
    {
        if (IsValidPos(piece.position, piece))
        {
            for (int i = 0; i < piece.cells.Length; i++)
            {
                Vector3Int currentPosition = piece.cells[i] + piece.position;
                this.tilemap.SetTile(currentPosition, piece.data.tile);
            }
        }
    }

    public void Clear(Piece piece)
    {
        if (IsValidPos(piece.position, piece))
        {
            for (int i = 0; i < piece.cells.Length; i++)
            {
                Vector3Int tilePosition = piece.cells[i] + piece.position;
                this.tilemap.SetTile(tilePosition, null);
            }
        }
    }

    public bool IsValidPos(Vector3Int position, Piece piece)
    {
        Vector3Int currentPosition;
        for (int i = 0; i < piece.cells.Length; i++)
        {
            currentPosition = piece.cells[i] + position;
            if (currentPosition.x <= leftBorder || currentPosition.x >= rightBorder)
                return false;
            if (currentPosition.y >= upperBorder || currentPosition.y <= lowerBorder)
                return false;
        }
        return true;
    }
}
