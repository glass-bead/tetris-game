using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public GameObject[] tetrominoList;
    private GameObject tetromino;

    private Vector3 spawnPos = new(-5f, 8f, 0f);

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
}
