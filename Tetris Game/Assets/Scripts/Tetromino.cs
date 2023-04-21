using System;
using UnityEngine;

public class Tetromino : MonoBehaviour
{
    private const float fallDelay = 0.8f;
    private const float moveDelay = 0.1f;
    private float fallTime;
    private float moveTime;

    // Start is called before the first frame update
    void Start()
    {
        moveTime = Time.time + moveDelay;
        fallTime = Time.time + fallDelay;
    }

    // Update is called once per frame
    void Update()
    {
        // Allow the player to hold movement keys but with move delay
        if (Time.time > moveTime)
        {
            HandleUserInputs();
        }

        // Move tetromino down every x seconds
        if (Time.time > fallTime && !Input.GetKey(KeyCode.DownArrow))
        {
            Falling();
        }
    }

    private void HandleUserInputs()
    {

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-1, 0, 0);
            moveTime = Time.time + moveDelay;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += new Vector3(1, 0, 0);
            moveTime = Time.time + moveDelay;
        }
        else if (Input.GetKey(KeyCode.DownArrow)) 
        {
            transform.position += new Vector3(0, -1, 0);
            moveTime = Time.time + moveDelay / 2;
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            Rotate();
        }

    }

    private void Falling()
    {
        transform.position += new Vector3(0, -1, 0);
        fallTime = Time.time + fallDelay;
    }

    private void Rotate()
    {
        throw new NotImplementedException();
    }
}
