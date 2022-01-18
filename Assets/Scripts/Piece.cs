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

    //private float update;
    //private float fallDelay;

    public float stepDelay = 1f;
    public float moveDelay = 0.1f;
    public float lockDelay = 0.5f;

    private bool checkStep = false;

    private float stepTime;
    private float moveTime;
    private float lockTime;

    public void Initialize(Board board, Vector3Int position, TetraminoTile data)
    {
        this.board = board;
        this.position = position;
        this.data = data;

        //update = Time.time;
        //fallDelay = 1.0f;

        this.stepTime = Time.time + this.stepDelay;
        this.moveTime = Time.time + this.moveDelay;
        this.lockTime = 0f;

        if (this.cells == null)
        {
            this.cells = new Vector3Int[data.cells.Length];
        }

        for (int i = 0; i < this.cells.Length; i++)
        {
            this.cells[i] = (Vector3Int)data.cells[i];
        }
    }

    private void UpdateTile(Vector3Int newPosition)
    {
        this.position = newPosition;
        this.board.Set(this);
    }

    /*private IEnumerator Wait()
     {
         //checkStep = true;
         yield return new WaitForSeconds(1);
         this.Step();
         StopCoroutine(Wait());
         //checkStep = false;
     }*/

    private void Update()
    {
        Step();
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
}
