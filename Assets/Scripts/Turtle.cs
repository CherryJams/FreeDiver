using System;
using UnityEngine;
using Random = UnityEngine.Random;


public class Turtle : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D rigidbody2d;
    private Vector3 direction;
    

    bool isRight
    {
        get { return (Random.value > 0.5f); }
    }

    private void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        direction = isRight ? Vector3.right : Vector3.left;
        if (direction==Vector3.left)
        {
            Flip();
        }
    }

    private void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;
    }

    private void FixedUpdate()
    {
        rigidbody2d.velocity = direction * speed;
    }
}