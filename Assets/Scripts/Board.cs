using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class Board : MonoBehaviour
{
    public Tilemap tilemap { get; set; }
    public TetraminoTile[] tetraminos;
    public Piece activePiece { get; set; }
    public Piece nextPiece { get; set; }
    public Vector3Int spawnPosition = new Vector3Int (4, 0, 0);
    public Vector3Int nextPieceSpawnPosition = new Vector3Int(12, 17, 0);


    public int leftBorder = 0;
    public int rightBorder = 9;
    public int upperBorder = 19;
    public int lowerBorder = 0;

    public float speed = 1.0f;

    public int score;
    public int highScore;
    public Text scoreText;
    public Text highScoreText;



    private void Awake()
    {
        this.tilemap = GetComponentInChildren<Tilemap>();
        this.activePiece = GetComponentInChildren<Piece>();
        this.nextPiece = this.gameObject.AddComponent<Piece>();
        nextPiece.enabled = false;

        for (int i = 0; i < this.tetraminos.Length; i++)
        {
            this.tetraminos[i].Initialize();
        }
    }

    private void Start()
    {
        score = 0;
        scoreText.text = score.ToString();
        if (PlayerPrefs.HasKey("HighScore"))
        {
            highScore = PlayerPrefs.GetInt("HighScore", 0);
            highScoreText.text = highScore.ToString();
        }
        SetNextTile();
        Spawn();
    }

    public void Spawn()
    {
        CheckLine();
        this.activePiece.Initialize(this, spawnPosition, nextPiece.data);
        Set(this.activePiece);
        SetNextTile();
    }

    public void SetNextTile()
    {
        if (nextPiece.cells != null) 
            Clear(nextPiece);
        int rand = Random.Range(0, this.tetraminos.Length);
        TetraminoTile newTile = this.tetraminos[rand];
        this.nextPiece.Initialize(this, this.nextPieceSpawnPosition, newTile);
        Set(nextPiece);
    }

    public void Set(Piece piece)
    {
        for (int i = 0; i < piece.cells.Length; i++)
        {
             Vector3Int currentPosition = piece.cells[i] + piece.position;
             this.tilemap.SetTile(currentPosition, piece.data.tile);
        }

    }

    public void Clear(Piece piece)
    {
        for (int i = 0; i < piece.cells.Length; i++)
        {
             Vector3Int tilePosition = piece.cells[i] + piece.position;
             this.tilemap.SetTile(tilePosition, null);
        }
    }

    public void ClearLine(int y)
    {
        Vector3Int position = new Vector3Int(0, y, 0);
        for (int i = leftBorder; i <= rightBorder; i++)
        {
            position.x = i;
            this.tilemap.SetTile(position, null);
        }

        Vector3Int newPosition = new Vector3Int(0, 0, 0);
        for (int i = y + 1; i <= upperBorder; i++)
        {
            position.y = i;
            newPosition.y = position.y - 1;
            for (int j = leftBorder; j <= rightBorder; j++)
            {
                position.x = j;
                newPosition.x = position.x;
                this.tilemap.SetTile(newPosition, tilemap.GetTile(position));
                if (position.y == upperBorder)
                    this.tilemap.SetTile(position, null);
            }
        }
    }

    public void CheckLine()
    {
        int check;
        Vector3Int position = new Vector3Int(0, 0, 0);
        int i = lowerBorder;
        while (i <= rightBorder)
        { 
            check = 0;
            position.y = i;
            for (int j = leftBorder; j <= rightBorder; j++)
            {
                position.x = j;
                if (this.tilemap.HasTile(position))
                    check++;
            }

            if (check == 10)
            {
                this.ClearLine(i);
                score += 10;
                scoreText.text = score.ToString();
                if (score > highScore)
                {
                    highScore = score;
                    highScoreText.text = highScore.ToString();
                    PlayerPrefs.SetInt("HighScore", highScore);
                    PlayerPrefs.Save();
                }
            }
            else i++;
        }
    }

    public bool IsValidPos(Vector3Int position, Piece piece)
    {
        Vector3Int currentPosition;
        Clear(activePiece);
        for (int i = 0; i < piece.cells.Length; i++)
        {
            currentPosition = piece.cells[i] + position;
            if (currentPosition.x <= leftBorder || currentPosition.x >= rightBorder)
                return false;
            if (currentPosition.y >= upperBorder || currentPosition.y <= lowerBorder)
                return false;
            if (this.tilemap.HasTile(currentPosition))
                return false;
        }
        return true;
    }
}
