using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeHead : MonoBehaviour
{

    enum Direction
    {
        up,
        down,
        left,
        right
    }

    Direction direction;

    public List<Transform> Tail = new List<Transform>();

    public float frameRate = 0.2f;
    public float step = .16f;

    public GameObject TailPrefab;

    public Vector2 horizontalRange;
    public Vector2 verticalRange;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Move", frameRate, frameRate);
    }

    void Move()
    {
        lastPos = transform.position;
        Vector3 nextPos = Vector3.zero;
        if (direction == Direction.up)
        {
            nextPos = Vector3.up;
        }
        else if (direction == Direction.down)
        {
            nextPos = Vector3.down;
        }
        else if (direction == Direction.left)
        {
            nextPos = Vector3.left;
        }
        else if (direction == Direction.right)
        {
            nextPos = Vector3.right;
        }
        nextPos *= step;

        transform.position += nextPos;

        MoveTail();
    }

    Vector3 lastPos;
    void MoveTail()
    {
        for (int i = 0; i < Tail.Count; i++)
        {

            Vector3 temp = Tail[i].position;
            Tail[i].position = lastPos;
            lastPos = temp;

        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            direction = Direction.up;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            direction = Direction.down;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            direction = Direction.left;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            direction = Direction.right;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Block"))
        {
            Debug.Log("Has perdido");
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
        else if (collision.CompareTag("Food"))
        {

            Tail.Add(Instantiate(TailPrefab, Tail[Tail.Count - 1].position, Quaternion.identity).transform);
            collision.transform.position = new Vector2(Random.Range(horizontalRange.x, horizontalRange.y), Random.Range(verticalRange.x, verticalRange.y));
            
        }

    }
}
