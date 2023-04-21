using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    private static readonly int width = 10, height = 20;
    private static Transform[,] grid = new Transform[width, height];

    public GameObject[] tetrominoList;
    private Vector3 spawnPos = new(4f, 18f, 0f);
    
    // Start is called before the first frame update
    void Start()
    {
        Spawn();
    }


    // Update is called once per frame
    void Update()
    {
        // TODO
    }

    // Spawn tetromino on the board
    private void Spawn() {

        // Select random tetromino to spawn from tetrominos' list
        int spawnIndex = Random.Range(0, 6);

        // Instantiate tetromino
        var currTetromino = Instantiate(tetrominoList[spawnIndex], spawnPos, Quaternion.identity);
        currTetromino.AddComponent<Tetromino>();
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

    internal bool IsValidMovement()
    {
        throw new System.NotImplementedException();
    }
}
