using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Board : MonoBehaviour
{
    public Tilemap tilemap { get; set; }
    public TetraminoTile[] tetraminos;
    public Piece activePiece { get; set; }
    public Vector3Int spawnPosition;


    private void Awake()
    {
        this.tilemap = GetComponentInChildren<Tilemap>();
        this.activePiece = GetComponentInChildren<Piece>();

        for (int i = 0; i < this.tetraminos.Length; i++)
        {
            this.tetraminos[i].Initialize();
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
        Set(this.activePiece);
    }

    public void Set(Piece piece)
    {
        for (int i = 0; i < piece.cells.Length; i++)
        {
            Vector3Int currentPosition = piece.cells[i] + piece.position;
            this.tilemap.SetTile(currentPosition, piece.data.tile);
        }
    }
}
