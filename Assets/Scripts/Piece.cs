using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    public Grid board { get; set; }
    public TetraminoTile data { get; set; }
    public Vector3Int[] cells { get; set; }
    public Vector3Int position { get; set; }

    public void Initialize(Grid board, Vector3Int position, TetraminoTile data)
    {
        this.board = board;
        this.position = position;
        this.data = data;

        if (this.cells == null)
        {
            this.cells = new Vector3Int[data.cell.Length];
        }

    }
}
