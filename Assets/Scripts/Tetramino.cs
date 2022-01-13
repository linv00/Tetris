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
    public Tetraminos Tetramino;
    public Tile TetraTile;
    public Vector2[] Tiles;
}
