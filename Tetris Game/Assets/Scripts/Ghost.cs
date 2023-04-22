using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    Board board;
    private Transform pivot;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.tag = "Ghost";

        board = GameObject.FindGameObjectWithTag("Board").GetComponent<Board>();
        pivot = transform.Find("Pivot");

        ChangeColor();
    }

    void LateUpdate()
    {
        Drop();
    }

    private void Drop()
    {
        while (board.IsValidMovement(pivot))
        {
            transform.position += new Vector3(0, -1, 0);
        }

        transform.position += new Vector3(0, 1, 0);
    }

    private void ChangeColor()
    {
        foreach (Transform child in pivot.transform)
        {
            child.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.15f);
        }
    }

    internal void DestroyGhost()
    {
        enabled = false;
        Destroy(gameObject);
    }
}
