using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField] ScoreBoard scoreboard;
    [SerializeField] AudioManager audioManager;

    private static readonly int width = 10, height = 20;
    private static Transform[,] grid = new Transform[width, height];

    internal void AddToGrid(Transform pivot)
    {
        foreach (Transform children in pivot.transform)
        {
            int x = Mathf.RoundToInt(children.transform.position.x);
            int y = Mathf.RoundToInt(children.transform.position.y);

            grid[x, y] = children;
        }
    }

    internal void CleanGrid()
    {
        grid = new Transform[width, height];
    }

    internal void CheckForLines()
    {
        bool _foundLines = false;

        for (int y = 0; y < height; y++)
        {
            if (HasLine(y))
            {
                _foundLines = true;

                // Delete line and move the rows above down one line
                DeleteLine(y);
                RowDown(y); 
            }
        }

        // If lines were found, check again
        if (_foundLines) CheckForLines();
    }

    private bool HasLine(int y)
    {
        // Check if all x pos of a y line have pieces the grid
        for (int x = 0; x < width; x++)
        {
            if (grid[x, y] == null)
            {
                return false;
            }
        }
        return true;
    }

    private void DeleteLine(int y)
    {
        for (int x = 0; x < width; x++)
        {
            Destroy(grid[x, y].gameObject);
            grid[x, y] = null;
        }

        scoreboard.UpdateLines();
        audioManager.PlaySound("clean line");
    }

    private void RowDown(int line)
    {
        for (int y = line; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (grid[x, y] != null)
                {
                    grid[x, y - 1] = grid[x, y];
                    grid[x, y] = null;
                    grid[x, y - 1].transform.position += new Vector3(0, -1, 0);
                }
            }
        }
    }

    internal bool IsValidMovement(Transform pivot)
    {
        foreach (Transform children in pivot.transform)
        {
            int x = Mathf.RoundToInt(children.transform.position.x);
            int y = Mathf.RoundToInt(children.transform.position.y);

            // If a piece is outside of grid, dont allow movement
            if (x < 0 || x >= width || y < 0)
            {
                return false;
            }

            // If there's another piece it this spot, dont allow movement
            if (grid[x, y] != null)
            {
                return false;
            }
        }

        return true;
    }
}
