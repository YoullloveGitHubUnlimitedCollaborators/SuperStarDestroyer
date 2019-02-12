using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Direction movementDirection;
    public float speed;
    public float destroyTime;

    Rigidbody2D rb;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, destroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.isGamePaused)
            switch (movementDirection)
            {
                case Direction.up:
                    rb.velocity = transform.up * (speed * Time.deltaTime);
                    break;

                case Direction.right:
                    rb.velocity = transform.right * (speed * Time.deltaTime);
                    break;

                case Direction.left:
                    rb.velocity = transform.right * (-speed * Time.deltaTime);
                    break;

                case Direction.down:
                    rb.velocity = transform.up * (-speed * Time.deltaTime);
                    break;
            }
    }
}

public enum Direction
{
    up,
    right,
    left,
    down
}
