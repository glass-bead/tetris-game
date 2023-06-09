using System;
using UnityEngine;

public class Tetromino : MonoBehaviour
{
    private float fallDelay = 1f;
    private const float moveDelay = 0.1f;
    private float fallTime, moveTime;

    private Board board;
    private Game game;
    private ScoreBoard scoreboard;
    private AudioManager audioManager;
    private Transform pivot;
    
    void Start()
    {
        gameObject.tag = "FallingTetromino";

        moveTime = Time.time + moveDelay;
        fallTime = Time.time + fallDelay;

        board = GameObject.FindGameObjectWithTag("Board").GetComponent<Board>();
        game = GameObject.FindGameObjectWithTag("GameController").GetComponent<Game>();
        scoreboard = GameObject.FindGameObjectWithTag("ScoreBoard").GetComponent<ScoreBoard>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

        // Get Tetromino's pivot
        pivot = transform.Find("Pivot");

        Falling();
    }

    void Update()
    {
        // Allow the player to hold movement keys but with move     delay
        if (Time.time > moveTime && game.isPaused == false)
        {
            HandleUserInputs();
        }

        // Move Tetromino down every x seconds
        if (Time.time > fallTime && game.isPaused == false)
        {
            Falling();
        }
    }

    private void HandleUserInputs()
    {

        Vector3 currentPos = transform.position;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            audioManager.PlaySound("move");
            transform.position += new Vector3(-1, 0, 0);
            moveTime = Time.time + moveDelay;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            audioManager.PlaySound("move");
            transform.position += new Vector3(1, 0, 0);
            moveTime = Time.time + moveDelay;
        }
        else if (Input.GetKey(KeyCode.DownArrow)) 
        {
            audioManager.PlaySound("soft drop");
            transform.position += new Vector3(0, -1, 0);
            scoreboard.UpdateScore(1);
            moveTime = Time.time + moveDelay / 2;

            if (!board.IsValidMovement(pivot))
            {
                scoreboard.UpdateScore(-1);
                transform.position = currentPos;
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Rotate();
            moveTime = Time.time + moveDelay;
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            HardDrop(); 
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            game.PauseButton();
        }
        else if (Input.GetKeyDown(KeyCode.M))
        {
            audioManager.ChangeSound();
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

        float level = scoreboard.level;
        fallDelay = (float)Math.Pow(0.8f - ((level - 1f) * 0.007f), level - 1f);
        fallTime = Time.time + fallDelay;

        if (!board.IsValidMovement(pivot))
        {
            transform.position = currentPos;
            Lock();
        }
    }

    private void Rotate()
    {
        audioManager.PlaySound("rotate");
        pivot.transform.Rotate(0, 0, -90);

        if (!board.IsValidMovement(pivot))
        {
            // Attempt wall kick by moving right
            transform.position += new Vector3(1, 0, 0);

            if (!board.IsValidMovement(pivot))
            {
                //Attempt wall kick by moving left
                transform.position += new Vector3(-2, 0, 0);

                if (!board.IsValidMovement(pivot))
                {
                    // Place tetromino back in its original place
                    transform.position += new Vector3(1, 0, 0);
                    pivot.transform.Rotate(0, 0, 90);
                }
            }       
        }
    }

    private void HardDrop()
    {
        while (board.IsValidMovement(pivot))
        {
            transform.position += new Vector3(0, -1, 0);
            scoreboard.UpdateScore(2);
        }

        transform.position += new Vector3(0, 1, 0);
        scoreboard.UpdateScore(-2);
        fallTime = Time.time + moveDelay;
    }

    private void Lock()
    {
        audioManager.PlaySound("locking");

        // Update grid and check for lines
        board.AddToGrid(pivot);
        board.CheckForLines();

        gameObject.tag = "Tetromino";

        // Destroy ghost tetromino 
        GameObject.FindGameObjectWithTag("Ghost").GetComponent<Ghost>().DestroyGhost();

        enabled = false;

        // Spawn the next piece
        board.RemoveNext();
        board.Spawn();
    }

}
