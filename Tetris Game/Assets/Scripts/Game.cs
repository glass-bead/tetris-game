using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Game : MonoBehaviour
{ 
    public GameObject[] tetrominoList;
    private List<int> bag, shuffledBag;
    private Vector3 spawnPos = new(4f, 18f, 0f);


    // Start is called before the first frame update
    void Start()
    {
        // Shuffle bag to generate a random 7 piece sequence
        bag = Enumerable.Range(0, tetrominoList.Length).ToList();
        ShuffleTetrominos(bag);

        Spawn();
    }


    // Update is called once per frame
    void Update()
    {
        // TODO
    }

    // Spawn Tetromino on the board
    internal void Spawn() {

        if (shuffledBag.Count <= 0)
        {
            ShuffleTetrominos(bag);
        }

        // Pull Tetromino from shuffled bag   
        var currTetromino = Instantiate(tetrominoList[shuffledBag[0]], spawnPos, Quaternion.identity);
        currTetromino.AddComponent<Tetromino>();

        shuffledBag.RemoveAt(0);
    }

    private void ShuffleTetrominos(List<int> values) 
    {
        Random rand = new Random();
        shuffledBag = values.OrderBy(_ => rand.Next()).ToList();

        Debug.Log(string.Join(", ", shuffledBag));
    }
  
}
