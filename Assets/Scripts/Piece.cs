using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    public Board board { get; set; }
    public TetraminoTile data { get; set; }
    public Vector3Int[] cells { get; set; }
    public Vector3Int position { get; set; }

    public void Initialize(Board board, Vector3Int position, TetraminoTile data)
    {
        this.board = board;
        this.position = position;
        this.data = data;

        if (this.cells == null)
        {
            this.cells = new Vector3Int[data.cells.Length];
        }

        for (int i = 0; i < this.cells.Length; i++)
        {
            this.cells[i] = (Vector3Int)data.cells[i];
        }
    }
}
