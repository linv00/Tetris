using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Grid : MonoBehaviour
{
    public Tilemap tilemap { get; set; }
    public TetraminoTile[] tetraminos;

    private void Awake()
    {
        this.tilemap = GetComponentInChildren<Tilemap>();

    }

    private void Start()
    {
        Spawn();
    }

    public void Spawn()
    {
        int rand = Random.Range(0, 6);
        TetraminoTile newTile = this.tetraminos[rand];
    }
}
