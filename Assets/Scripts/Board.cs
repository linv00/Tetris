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
    public int activePieceNum;

    private static readonly int height = 20;
    private static readonly int width = 10;
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
        activePieceNum = Random.Range(0, this.tetraminos.Length);
        TetraminoTile newTile = this.tetraminos[activePieceNum];
        this.activePiece.Initialize(this, this.spawnPosition, newTile);
        Set(this.activePiece);
    }

    public void Set(Piece piece)
    {
        for (int i = 0; i < piece.cells.Length; i++)
        {
            Vector3Int currentPosition = piece.cells[i] + piece.position;
            if (ValidOnUp(currentPosition))
            {
                this.tilemap.SetTile(currentPosition, piece.data.tile);
            }
        }
    }

    public void Clear(Piece piece)
    {
        for (int i = 0; i < piece.cells.Length; i++)
        {
            Vector3Int tilePosition = piece.cells[i] + piece.position;
            if (ValidOnUp(tilePosition))
            {
                this.tilemap.SetTile(tilePosition, null);
            }
        }
    }

    public bool ValidOnUp(Vector3Int position)
    {
        if (position.y < 1)
        {
            return true;
        }
        else return false;
    }
}
