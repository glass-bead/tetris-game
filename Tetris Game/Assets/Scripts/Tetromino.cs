using System;
using UnityEngine;

public class Tetromino : MonoBehaviour
{
    private const float fallDelay = 0.8f;
    private const float moveDelay = 0.1f;
    private float fallTime;
    private float moveTime;

    private Transform pivot;

    // Start is called before the first frame update
    void Start()
    {
        moveTime = Time.time + moveDelay;
        fallTime = Time.time + fallDelay;

        // Get Tetromino's pivot
        pivot = gameObject.transform.Find("Pivot");
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

        Vector3 currentPos = transform.position;

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
            moveTime = Time.time + moveDelay;
        }

        //if (!ValidMovement())
        //{
        //    transform.position = currentPos;
        //}

    }

    private void Falling()
    {
        transform.position += new Vector3(0, -1, 0);
        fallTime = Time.time + fallDelay;
    }

    private void Rotate()
    {
        pivot.transform.Rotate(0, 0, -90);
    }

    private bool ValidMovement()
    {
        throw new NotImplementedException();
    }
}
