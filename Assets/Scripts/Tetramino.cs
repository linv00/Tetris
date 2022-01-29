using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public enum Tetraminos
{
    L,
    J,
    Z, 
    S,
    T,
    I,
    O,
}

[System.Serializable]
public struct TetraminoTile
{
    public Tetraminos tetramino;
    public Tile tile;
    public Vector2Int[] cells { get; set; }

    public void Initialize()
    {
        int scale = ((CellData.mode) ? CellData.classicScale : CellData.chaoticScale);
        cells = new Vector2Int[scale * scale * CellData.Cells[this.tetramino].Length];
        for (int i = 0; i < CellData.Cells[this.tetramino].Length*scale*scale; i++)
        {
            cells[i] = Vector2Int.zero;
        }
        for (int i = 0; i < CellData.Cells[this.tetramino].Length; i++)
        {
            for (int j = 0; j < scale; j++)
            {
                for (int a = 0; a < scale; a++)
                {
                    cells[scale * scale * i + scale * j + a].x = CellData.Cells[this.tetramino][i].x*scale + j;
                    cells[scale * scale * i + scale * j + a].y = CellData.Cells[this.tetramino][i].y*scale + a;
                }
            }
        }
    }
}
