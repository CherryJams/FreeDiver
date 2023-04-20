using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private Animator animator;
    private Rigidbody2D rigidbody2D;
    private Vector3 movementVector;
    private bool facingRight = true;
    private bool isUnderwater = false;

    private void Awake()
    {
        isUnderwater = false;
        rigidbody2D = GetComponent<Rigidbody2D>();
        movementVector = new Vector3();
    }

    void FixedUpdate()
    {
        movementVector.x = Input.GetAxis("Horizontal");
        movementVector.y=0;
        if (isUnderwater)
        {
            movementVector.y = Input.GetAxis("Vertical");
        }

        movementVector *= speed;
        rigidbody2D.velocity = movementVector;
        if (IsPlayerFacingOppositeDirectionOfMovement())
        {
            Flip();
        }

        if (Input.GetKeyDown(KeyCode.Space) && !isUnderwater)
        {
            isUnderwater = true;
            animator.SetBool("isUnderwater", true);
            AudioManager.GetInstance().GetAudioSource("Ambience","SeaWaves").Stop();
            AudioManager.GetInstance().GetAudioSource("Ambience","Abyss").Play();
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
        return movementVector.x > 0 && !facingRight || movementVector.x < 0 && facingRight;
    }
    public void SetIsUnderwater(bool isUnderwater)
    {
        this.isUnderwater = isUnderwater;
    }
}