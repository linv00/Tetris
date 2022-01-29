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
    private float moveTimer = 0;
     

    private bool moveDown = false;
    private bool moveRight = false;
    private bool moveLeft = false;
    private int isRotate;

    public void Initialize(Board board, Vector3Int position, TetraminoTile data)
    {
        this.board = board;
        this.position = position;
        this.data = data;
        this.isRotate = -1;
  
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

    private bool WaitedMove(float seconds)
    {
        moveTimer += Time.deltaTime;

        if (moveTimer >= seconds)
        {
            return true;
        }
        return false;
    }

    private void Update()
    {
        if (moveDown)
        {
            timer += 7*Time.deltaTime;
        }
        if (isInit && board.game)
        {
            Vector3Int newPosition = position;
            if(moveRight && WaitedMove(board.moveSpeed))
            {
                newPosition.x++;
                moveTimer = 0;
            }
            if(moveLeft && WaitedMove(board.moveSpeed))
            {
                newPosition.x--;
                moveTimer = 0;
            }
            if (board.IsValidPos(newPosition, this))
                UpdateTile(newPosition);
            else UpdateTile(position);

            if (!Waited(this.board.speed)) return;
            else
            {
                Step();
                timer = 0;
            }
        }
    }

    public void Step()
    {
        Vector3Int newPosition = position;
        newPosition.y--;
        if (board.IsValidPos(newPosition, this))
            UpdateTile(newPosition);
        else
        {
            UpdateTile(position);
            this.board.speed *= 0.991f;
            if (!board.IsGameOver())
            {
                board.Spawn();
            }
            else board.GameOver();
        }
    }

    public void MoveRight()
    {
        moveRight = true;
    }

    public void StopMoveRight()
    {
        moveRight = false;
    }  

    public void MoveRightOnClick()
    {
        Vector3Int newPosition = position;
        newPosition.x++;
        if (board.IsValidPos(newPosition, this))
            UpdateTile(newPosition);
        else
        {
            UpdateTile(position);
        }
    }

    public void MoveLeftOnClick()
    {
        Vector3Int newPosition = position;
        newPosition.x--;
        if (board.IsValidPos(newPosition, this))
            UpdateTile(newPosition);
        else
        {
            UpdateTile(position);
        }
    }

    public void MoveLeft()
    {
        moveLeft = true;
    }

    public void StopMoveLeft()
    {
        moveLeft = false;
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
        if (board.game)
        {
            Vector3Int[] cell = new Vector3Int[this.cells.Length];
            if (this.data.tetramino != Tetraminos.O)
            {
                for (int i = 0; i < this.cells.Length; i++)
                {
                    cell[i] = this.cells[i];
                    int x, y;
                    if (isRotate == -1)
                    {
                        x = Mathf.RoundToInt((cell[i].x * CellData.rotateRightMatrix[0]) + (cell[i].y * CellData.rotateRightMatrix[1]));
                        y = Mathf.RoundToInt((cell[i].x * CellData.rotateRightMatrix[2]) + (cell[i].y * CellData.rotateRightMatrix[3]));
                    }
                    else
                    {
                        x = Mathf.RoundToInt((cell[i].x * CellData.rotateLeftMatrix[0]) + (cell[i].y * CellData.rotateLeftMatrix[1]));
                        y = Mathf.RoundToInt((cell[i].x * CellData.rotateLeftMatrix[2]) + (cell[i].y * CellData.rotateLeftMatrix[3]));
                    }
                    cell[i].x = x;
                    cell[i].y = y;
                }
            }

            if (board.IsValidPos(position, cell))
            {
                this.cells = cell;       
                if (this.data.tetramino != Tetraminos.L && this.data.tetramino != Tetraminos.J && this.data.tetramino != Tetraminos.T)
                    isRotate *= -1;
            }
            board.Set(this);
        }
    }
}
