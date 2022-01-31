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
    public Vector3Int spawnPosition;
    public Vector3Int nextPieceSpawnPosition;


    public int leftBorder;
    public int rightBorder;
    public int upperBorder;
    public int lowerBorder;

    public float speed;
    public float moveSpeed;

    public int score;
    public int highScore;
    string record;
    public Text scoreText;
    public Text highScoreText;

    public Text gameOver;
    public bool game = true;

    public GameObject grid;
    public int scale;

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

        if (CellData.mode)
        {
            scale = CellData.classicScale;
        }
        else
        {
            scale = CellData.chaoticScale;
        }
        grid.transform.localScale = new Vector3((float)1/scale, (float)1/scale, (float)1/scale);
        leftBorder = 0 * scale;
        rightBorder = 10 * scale;
        upperBorder = 20 * scale;
        lowerBorder = 0 * scale;
        spawnPosition = new Vector3Int(4 * scale, 21 * scale, 0);
        nextPieceSpawnPosition = new Vector3Int(12 * scale, 17 * scale, 0);
        tilemap.gameObject.transform.localPosition = new Vector3Int(-7 * scale, -8 * scale, 0);
        speed = 1.0f / scale;
        moveSpeed = 0.3f / scale;

}

    private void Start()
    {
        score = 0;
        scoreText.text = score.ToString();
        gameOver.text = "";
        if (CellData.mode) record = "HighScoreClassic";
        else record = "HighScoreChaotic";

        if (PlayerPrefs.HasKey(record))
        {
            highScore = PlayerPrefs.GetInt(record, 0);
            highScoreText.text = highScore.ToString();
        }
        else
        {
            highScore = score;
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
            if(currentPosition.y < upperBorder)
            {
                this.tilemap.SetTile(currentPosition, piece.data.tile);
            }
        }
    }

    public void GameOver()
    {
        game = false;
        tilemap.gameObject.layer = 1;
        gameOver.text = "GAME OVER";
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

        Vector3Int newPosition = Vector3Int.zero;
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
        Vector3Int position = Vector3Int.zero;
        int i = lowerBorder;
        while (i <= upperBorder)
        { 
            check = 0;
            position.y = i;
            for (int j = leftBorder; j <= rightBorder; j++)
            {
                position.x = j;
                if (this.tilemap.HasTile(position))
                    check++;
            }

            if (check == 10*scale)
            {
                this.ClearLine(i);
                score += 10;
                scoreText.text = score.ToString();
                if (score > highScore)
                {
                    highScore = score;
                    highScoreText.text = highScore.ToString();
                    PlayerPrefs.SetInt(record, highScore);
                    PlayerPrefs.Save();
                }
            }
            else i++;
        }
    }

    public bool IsValidPos(Vector3Int position, Piece piece)  // для движения
    {
        Vector3Int currentPosition;
        Clear(activePiece); 
        for (int i = 0; i < piece.cells.Length; i++)
        {
            currentPosition = piece.cells[i] + position;
            if (currentPosition.x < leftBorder || currentPosition.x >= rightBorder)
                return false;
            if (currentPosition.y < lowerBorder)
                return false;
            if (this.tilemap.HasTile(currentPosition))
                return false;
        }
        return true;
    }

    public bool IsValidPos(Vector3Int position, Vector3Int[] cells)  // для поворота
    {
        Vector3Int currentPosition;
        Clear(activePiece);
        for (int i = 0; i < cells.Length; i++)
        {
            currentPosition = cells[i] + position;
            if (currentPosition.x < leftBorder || currentPosition.x >= rightBorder)
                return false;
            if (currentPosition.y < lowerBorder)
                return false;
            if (this.tilemap.HasTile(currentPosition))
                return false;
        }
        return true;
    }

    public bool IsGameOver()
    {
        Vector3Int position = Vector3Int.zero;
        position.y = upperBorder - 1;
        for (int i = leftBorder; i <= rightBorder; i++)
        {
            position.x = i;
            if (this.tilemap.HasTile(position)) return true;
        }
        return false;
    }
}
