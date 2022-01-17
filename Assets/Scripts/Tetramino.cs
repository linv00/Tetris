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
        this.cells = CellData.Cells[this.tetramino];
    }

}
