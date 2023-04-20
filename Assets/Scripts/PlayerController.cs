using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    private Rigidbody2D rigidbody2D;
    private Vector3 movementVector;
    private bool facingRight = true;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        movementVector = new Vector3();
    }

    void FixedUpdate()
    {
        movementVector.x = Input.GetAxis("Horizontal");
        movementVector.y = Input.GetAxis("Vertical");
        movementVector *= speed;
        rigidbody2D.velocity = movementVector;
        if(IsPlayerFacingOppositeDirectionOfMovement())
        {
            Flip();
        }
    }

    private void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;
        facingRight = !facingRight;
    }

    private bool IsPlayerFacingOppositeDirectionOfMovement()
    {
        return movementVector.x>0 &&!facingRight || movementVector.x<0 && facingRight;
    }
}