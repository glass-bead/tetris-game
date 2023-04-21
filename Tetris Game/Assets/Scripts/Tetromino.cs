using UnityEngine;

public class Tetromino : MonoBehaviour
{
    private const float fallTime = 0.8f;
    private float previousTime = fallTime;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        Falling();
    }

    private void Falling()
    {
        // Makes the tetromino fall. Accelerates movement with user input.
        if (Time.time - previousTime > (Input.GetKey(KeyCode.DownArrow) ? fallTime / 20 : fallTime))
        {
            transform.position += new Vector3(0, -1, 0);
            previousTime = Time.time;
        }
    }

}
