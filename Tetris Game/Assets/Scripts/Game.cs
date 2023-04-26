using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Game : MonoBehaviour
{ 
    
    [SerializeField] GameObject gamePanel, gameoverPanel, pausePanel, htpPanel, pauseButton, score, level, lines;
    [SerializeField] GameObject gameOverTitle, pauseTitle;
    [SerializeField] Tweening tween;
    [SerializeField] Board board;
    [SerializeField] AudioManager audioManager;

    public bool isPaused = false;

    private void Start()
    {
        audioManager.PlaySound("main menu");
    }

    internal void GameOver()
    {
        audioManager.StopSound("theme");
        audioManager.PlaySound("game over");

        // Show Game Over screen
        gameoverPanel.SetActive(true);
        pauseButton.SetActive(false);
        tween.PulsatingTitle(gameOverTitle);
    }

    public void PlayButton()
    {
        audioManager.StopSound("main menu");
        audioManager.PlaySound("theme");
        gamePanel.SetActive(false);
        pauseButton.SetActive(true);
        score.SetActive(true);
        level.SetActive(true);
        lines.SetActive(true);
        board.BeginGame();
    }

    public void PauseButton()
    {
        audioManager.PauseSound("theme");
        audioManager.PlaySound("pause");
        isPaused = true;
        pausePanel.SetActive(true);
        pauseButton.SetActive(false);
        tween.PulsatingTitle(pauseTitle);
    }

    public void ResumeButton()
    {
        audioManager.PlaySound("theme");
        isPaused = false;
        pausePanel.SetActive(false);
        pauseButton.SetActive(true);
    }

    public void PlayAgainButton()
    {
        audioManager.PlaySound("theme");
        gameoverPanel.SetActive(false);
        pauseButton.SetActive(true);
        board.BeginGame();
    }

 
    public void HtpButton ()
    {
        if (htpPanel.activeSelf)
        {
            htpPanel.SetActive(false);
        } else
        {
            htpPanel.SetActive(true);
        }
    }

    public void MainMenuButton()
    {
        audioManager.StopSound("theme");
        audioManager.PlaySound("main menu");
        isPaused = false;
        board.CleanBoard();
        gameoverPanel.SetActive(false);
        pausePanel.SetActive(false);
        gamePanel.SetActive(true);
    }

    public void QuitButton()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
            Application.Quit();
    }

    
}
