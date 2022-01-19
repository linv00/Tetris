using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Piece : MonoBehaviour
{
    public Board board { get; set; }
    public TetraminoTile data { get; set; }
    public Vector3Int[] cells { get; set; }
    public Vector3Int position { get; set; }

    bool isInit = false;
    private float timer = 0;
    private bool moveDown = false;
    private int isRotate = -1;

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
        isInit = true;
    }

    private void UpdateTile(Vector3Int newPosition)
    {
        this.position = newPosition;
        this.board.Set(this);
    }

    private bool Waited(float seconds)
    {
        timer += Time.deltaTime;

        if (timer >= seconds)
        {
            return true; 
        }

        return false;
    }

    private void Update()
    {
        if (moveDown)
        {
            timer += 5*Time.deltaTime;
        }
        if (!Waited(1)) return;
        else
        {
            if (isInit)
            {
                Step();
            }
            timer = 0;
        }
    }

    public void Step()
    {
        this.board.Clear(this);
        Vector3Int newPosition = position;
        newPosition.y--;
        UpdateTile(newPosition);
    }

    public void MoveRight()
    {
        this.board.Clear(this);
        Vector3Int newPosition = position;
        newPosition.x++;
        UpdateTile(newPosition);
    }

    public void MoveLeft()
    {
        this.board.Clear(this);
        Vector3Int newPosition = position;
        newPosition.x--;
        UpdateTile(newPosition);
    }

    public void MoveDown()
    {
        moveDown = true;
    }

    public void StopMoveDown()
    {
        moveDown = false;
    }

    public void Rotate()
    {
        this.board.Clear(this);
        if (this.data.tetramino != Tetraminos.O)
        {
            for (int i = 0; i < this.cells.Length; i++)
            {
                Vector3Int cell = this.cells[i];

                int x, y;
                if (isRotate == -1)
                {
                    x = Mathf.RoundToInt((cell.x * CellData.rotateRightMatrix[0]) + (cell.y * CellData.rotateRightMatrix[1]));
                    y = Mathf.RoundToInt((cell.x * CellData.rotateRightMatrix[2]) + (cell.y * CellData.rotateRightMatrix[3]));
                    this.cells[i] = new Vector3Int(x, y, 0);
                }
                else
                {
                    x = Mathf.RoundToInt((cell.x * CellData.rotateLeftMatrix[0]) + (cell.y * CellData.rotateLeftMatrix[1]));
                    y = Mathf.RoundToInt((cell.x * CellData.rotateLeftMatrix[2]) + (cell.y * CellData.rotateLeftMatrix[3]));
                    this.cells[i] = new Vector3Int(x, y, 0);
                }
                

            }
            if (this.data.tetramino != Tetraminos.L && this.data.tetramino != Tetraminos.J && this.data.tetramino != Tetraminos.T)
                isRotate *= -1;
        }
        UpdateTile(position);
    }
}
