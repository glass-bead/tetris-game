using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
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
