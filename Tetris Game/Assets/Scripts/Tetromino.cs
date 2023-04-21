using System;
using UnityEngine;

public class Tetromino : MonoBehaviour
{
    private const float fallDelay = 0.8f;
    private const float moveDelay = 0.1f;
    private float fallTime;
    private float moveTime;

    private Board board;
    private Game game;
    private Transform pivot;
    
    void Start()
    {
        moveTime = Time.time + moveDelay;
        fallTime = Time.time + fallDelay;

        board = GameObject.FindGameObjectWithTag("Board").GetComponent<Board>();
        game = GameObject.FindGameObjectWithTag("GameController").GetComponent<Game>();

        // Get tetromino's pivot
        pivot = transform.Find("Pivot");
    }

    void Update()
    {
        // Allow the player to hold movement keys but with move delay
        if (Time.time > moveTime)
        {
            HandleUserInputs();
        }

        // Move tetromino down every x seconds
        if (Time.time > fallTime && !Input.GetKey(KeyCode.DownArrow))
        {
            Falling();
        }
    }

    private void HandleUserInputs()
    {

        Vector3 currentPos = transform.position;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-1, 0, 0);
            moveTime = Time.time + moveDelay;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += new Vector3(1, 0, 0);
            moveTime = Time.time + moveDelay;
        }
        else if (Input.GetKey(KeyCode.DownArrow)) 
        {
            transform.position += new Vector3(0, -1, 0);
            moveTime = Time.time + moveDelay / 2;
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            Rotate();
            moveTime = Time.time + moveDelay;
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            HardDrop();
        }

        if (!board.IsValidMovement(pivot))
        {
            transform.position = currentPos;
        }

    }

    private void Falling()
    {
        Vector3 currentPos = transform.position;

        transform.position += new Vector3(0, -1, 0);
        fallTime = Time.time + fallDelay;

        if (!board.IsValidMovement(pivot))
        {
            transform.position = currentPos;

            // Add tetromino to grid
            board.AddToGrid(pivot);

            // Stop movement and spawn new tetromino
            Lock();
            game.Spawn();
        }
    }

    private void Rotate()
    {
        pivot.transform.Rotate(0, 0, -90);

        if (!board.IsValidMovement(pivot))
        {
            pivot.transform.Rotate(0, 0, 90);
        }
    }

    private void HardDrop()
    {
        while (board.IsValidMovement(pivot))
        {
            transform.position += new Vector3(0, -1, 0);
        }

        transform.position += new Vector3(0, 1, 0);
    }

    private void Lock()
    {
        this.enabled = false;
    }

}
