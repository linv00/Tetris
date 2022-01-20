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
    public int tetraNum;

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
        this.board.Clear(this);
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
        Vector3Int newPosition = position;
        newPosition.y--;
        if (board.IsValidPos(newPosition, this))
            UpdateTile(newPosition);
        else return;
    }

    public void MoveRight()
    {
        Vector3Int newPosition = position;
        newPosition.x++;
        if (board.IsValidPos(newPosition, this))
            UpdateTile(newPosition);
        else return;
    }

    public void MoveLeft()
    {
        Vector3Int newPosition = position;
        newPosition.x--;
        if (board.IsValidPos(newPosition, this))
            UpdateTile(newPosition);
        else return;
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
        Vector3Int[] cell = new Vector3Int[this.cells.Length];

 
        if (this.data.tetramino != Tetraminos.O)
        {
            for (int i = 0; i < this.cells.Length; i++)
            {
                cell[i] = this.cells[i];
                int x, y;
                if (isRotate == -1)
                {
                    x = Mathf.RoundToInt((cells[i].x * CellData.rotateRightMatrix[0]) + (cells[i].y * CellData.rotateRightMatrix[1]));
                    y = Mathf.RoundToInt((cells[i].x * CellData.rotateRightMatrix[2]) + (cells[i].y * CellData.rotateRightMatrix[3]));
                }
                else
                {
                    x = Mathf.RoundToInt((cells[i].x * CellData.rotateLeftMatrix[0]) + (cells[i].y * CellData.rotateLeftMatrix[1]));
                    y = Mathf.RoundToInt((cells[i].x * CellData.rotateLeftMatrix[2]) + (cells[i].y * CellData.rotateLeftMatrix[3]));
                }
                this.cells[i].x = x;
                this.cells[i].y = y;
            }
        }

        if (board.IsValidPos(position, this))
        {
            UpdateTile(position);
            if (this.data.tetramino != Tetraminos.L && this.data.tetramino != Tetraminos.J && this.data.tetramino != Tetraminos.T)
                isRotate *= -1;
        }   
        else
        {
            this.cells = cell;
            this.board.Set(this);
            return;
        }
    }
}
