using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Game : MonoBehaviour
{ 
    [SerializeField] GameObject[] tetrominoList;
    [SerializeField] GameObject gamePanel, gameoverPanel, pausePanel, pauseButton, score, level, lines;
    [SerializeField] Tweening tween;

    private GameObject currTetromino, nextTetromino, ghostTetromino;
    private ScoreBoard scoreboard;
    private Board board;
    private List<int> bag, shuffledBag;
   
    private Vector3 spawnPos = new(4f, 18f, 0f);
    private Vector3 nextPos = new(13.5f, 16.5f, 0f);

    public bool isPaused = false;

    void Start()
    {
        board = GameObject.FindGameObjectWithTag("Board").GetComponent<Board>();
        scoreboard = GameObject.FindGameObjectWithTag("ScoreBoard").GetComponent<ScoreBoard>();
    }

    internal void Spawn() {

        if (shuffledBag.Count <= 0)
        {
            ShuffleTetrominos(bag);
        }

        // Pull Tetromino from shuffled bag   
        currTetromino = Instantiate(tetrominoList[shuffledBag[0]], spawnPos, Quaternion.identity);
        currTetromino.AddComponent<Tetromino>();

        CreateGhost();
        shuffledBag.RemoveAt(0);

        if (shuffledBag.Count <= 0)
        {
            ShuffleTetrominos(bag);
        }

        nextTetromino = Instantiate(tetrominoList[shuffledBag[0]]);
        nextTetromino.transform.Find("Pivot").position = nextPos;

        // Check if Tetromino spawned overlapping other pieces 
        // or spawned outside the play zone
        if (!board.IsValidMovement(currTetromino.transform.Find("Pivot")))
        {
            ToppedOut();
        }
    }

    private void ShuffleTetrominos(List<int> values) 
    {
        Random rand = new Random();
        shuffledBag = values.OrderBy(_ => rand.Next()).ToList();
    }

    private void ToppedOut()
    {
        Debug.Log("Game Over");
        Destroy(currTetromino);
        Destroy(ghostTetromino);
        Destroy(nextTetromino);

        // Show Game Over screen
        gameoverPanel.SetActive(true);
        tween.GameOver();
    }

    private void CreateGhost()
    {
        ghostTetromino = Instantiate(tetrominoList[shuffledBag[0]], spawnPos, Quaternion.identity);
        ghostTetromino.AddComponent<Ghost>();
    }

    internal void RemoveNext()
    {
        Destroy(nextTetromino);
    }

    public void PlayButton()
    {
        gamePanel.SetActive(false);
        pauseButton.SetActive(true);
        score.SetActive(true);
        level.SetActive(true);
        lines.SetActive(true);

        // Shuffle bag to generate a random 7 piece sequence
        bag = Enumerable.Range(0, tetrominoList.Length).ToList();
        ShuffleTetrominos(bag);

        Spawn();
    }

    public void PauseButton()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            isPaused = true;

            // Show Pause screen
            pausePanel.SetActive(true);
        }
    }

    public void ResumeButton()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            isPaused = false;

            // Hide Pause screen
            pausePanel.SetActive(false);
        }
    }

    public void PlayAgainButton()
    {
        // Show Game Over screen
        gameoverPanel.SetActive(false);

        // Destroy all existing tetrominos
        GameObject[] tetrominosToDelete = GameObject.FindGameObjectsWithTag("Tetromino");
        foreach (GameObject tetromino in tetrominosToDelete) {
            Destroy(tetromino);
        }

        // Restart scoreboard values
        scoreboard.RestartValues();

        Spawn();
    }

}
