using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CellData
{
    public static float angle = Mathf.PI / (float)2;
    public static float[] Matrix = new float[] { Mathf.Cos(angle), -Mathf.Sin(angle), Mathf.Sin(angle), Mathf.Cos(angle)};

    public static readonly Dictionary<Tetraminos, Vector2Int[]> Cells = new Dictionary<Tetraminos, Vector2Int[]>()
    {
        { Tetraminos.L, new Vector2Int[] { new Vector2Int( 0, 1), new Vector2Int( 0, 0), new Vector2Int( 0, -1), new Vector2Int( 1, -1) } },
        { Tetraminos.J, new Vector2Int[] { new Vector2Int( 1, 1), new Vector2Int( 1, 0), new Vector2Int( 1, -1), new Vector2Int( 0, -1) } },
        { Tetraminos.Z, new Vector2Int[] { new Vector2Int( -1, 1), new Vector2Int( 0, 1), new Vector2Int( 0, 0), new Vector2Int( 1, 0) } },
        { Tetraminos.S, new Vector2Int[] { new Vector2Int( -1, 0), new Vector2Int( 0, 0), new Vector2Int( 0, 1), new Vector2Int( 1, 1) } },
        { Tetraminos.T, new Vector2Int[] { new Vector2Int( 0, 1), new Vector2Int( 1, 1), new Vector2Int( 2, 1), new Vector2Int( 1, 0) } },
        { Tetraminos.I, new Vector2Int[] { new Vector2Int( -1, 1), new Vector2Int( 0, 1), new Vector2Int( 1, 1), new Vector2Int( 2, 1) } },
        { Tetraminos.O, new Vector2Int[] { new Vector2Int( 1, 1), new Vector2Int( 0, 1), new Vector2Int( 1, 0), new Vector2Int( 0, 0) } },
    };
}
