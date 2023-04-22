using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Game : MonoBehaviour
{ 
    public GameObject[] tetrominoList;

    private GameObject currTetromino; 
    private Board board;
    private List<int> bag, shuffledBag;
    private Vector3 spawnPos = new(4f, 18f, 0f);

    void Start()
    {
        board = GameObject.FindGameObjectWithTag("Board").GetComponent<Board>();

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
    }

    internal void CreateGhost()
    {
        var ghostTetromino = Instantiate(tetrominoList[shuffledBag[0]], spawnPos, Quaternion.identity);
        ghostTetromino.AddComponent<Ghost>();
    }

}
