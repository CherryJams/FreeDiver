using System;
using UnityEngine;
using Random = UnityEngine.Random;


public class Fish : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int experienceReward;
    private Rigidbody2D rigidbody2d;
    private Vector3 direction;
    private GameObject player;


    bool isRight
    {
        get { return (Random.value > 0.5f); }
    }

    public int GetExperienceReward()
    {
        return experienceReward;
    }

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        rigidbody2d = GetComponent<Rigidbody2D>();
        direction = isRight ? Vector3.right : Vector3.left;
        if (direction == Vector3.left)
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
        if (Vector3.Distance(gameObject.transform.position, player.transform.position) > 30)
        {
            gameObject.SetActive(false);
        }
    }
}