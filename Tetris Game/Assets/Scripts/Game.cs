using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Game : MonoBehaviour
{ 
    [SerializeField] GameObject[] tetrominoList;
    [SerializeField] GameObject gamePanel, gameoverPanel, pausePanel, pauseButton, score, level, lines;
    [SerializeField] GameObject gameOverTitle, pauseTitle;
    [SerializeField] Tweening tween;
    [SerializeField] ScoreBoard scoreboard;
    [SerializeField] Board board;

    private GameObject currTetromino, nextTetromino, ghostTetromino;
    private List<int> bag, shuffledBag;
   
    private Vector3 spawnPos = new(4f, 18f, 0f);
    private Vector3 nextPos = new(13.5f, 16.5f, 0f);

    public bool isPaused = false;

    private void BeginGame()
    {
        // Shuffle bag to generate a random 7 piece sequence
        bag = Enumerable.Range(0, tetrominoList.Length).ToList();
        ShuffleTetrominos(bag);

        Spawn();
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

    private void CreateGhost()
    {
        ghostTetromino = Instantiate(tetrominoList[shuffledBag[0]], spawnPos, Quaternion.identity);
        ghostTetromino.AddComponent<Ghost>();
    }

    internal void RemoveNext()
    {
        Destroy(nextTetromino);
    }

    private void ToppedOut()
    {
        Destroy(currTetromino);
        Destroy(ghostTetromino);
        Destroy(nextTetromino);

        board.CleanGrid();

        // Show Game Over screen
        gameoverPanel.SetActive(true);
        pauseButton.SetActive(false);
        tween.PulsatingTitle(gameOverTitle);
    }

    public void PlayButton()
    {
        gamePanel.SetActive(false);
        pauseButton.SetActive(true);
        score.SetActive(true);
        level.SetActive(true);
        lines.SetActive(true);

        BeginGame();
    }

    public void PauseButton()
    {
        isPaused = true;
        pausePanel.SetActive(true);
        pauseButton.SetActive(false);
        tween.PulsatingTitle(pauseTitle);
    }

    public void ResumeButton()
    {
        isPaused = false;
        pausePanel.SetActive(false);
        pauseButton.SetActive(true);
    }

    public void PlayAgainButton()
    {
        // Destroy all existing tetrominos
        GameObject[] tetrominosToDelete = GameObject.FindGameObjectsWithTag("Tetromino");
        foreach (GameObject tetromino in tetrominosToDelete) {
            Destroy(tetromino);
        }

        // Restart scoreboard values
        scoreboard.RestartValues();

        // Hide Game Over screen
        gameoverPanel.SetActive(false);
        pauseButton.SetActive(true);

        BeginGame();
    }

    //// TODO
    //public void OptionsButton()
    //{

    //}

    //// TODO
    //public void QuitButton()
    //{ 

    //}

}
