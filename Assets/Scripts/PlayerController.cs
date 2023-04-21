using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private Animator animator;
    [SerializeField] private int diveForce = 5;
    private Rigidbody2D rigidbody2D;
    private BoxCollider2D boxCollider2D;
    private Vector3 movementVector;
    private AudioSource currentAudioSource;
    private bool facingRight = true;
    private bool isUnderwater = false;
    private AudioManager audioManager;

    private void Awake()
    {
        audioManager = AudioManager.GetInstance();
        isUnderwater = false;
        rigidbody2D = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        movementVector = new Vector3();
    }

    void FixedUpdate()
    {
        movementVector.x = Input.GetAxis("Horizontal");
        movementVector.y = 0;
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
            StartCoroutine(Dive(1f));
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

    private IEnumerator SmoothLerp(float time)
    {
        Vector3 startingPos = gameObject.transform.position;
        Vector3 finalPos = gameObject.transform.position + (transform.up * -2);
        float elapsedTime = 0;

        while (elapsedTime < time)
        {
            gameObject.transform.position = Vector3.Lerp(startingPos, finalPos, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator Dive(float time)
    {
        currentAudioSource = audioManager.GetAudioSource("SFX", "DeepBreath");
        currentAudioSource.Play();
        yield return WaitForSoundToFinish(currentAudioSource);
        audioManager.GetAudioSource("SFX", "Bubbles").Play();
        currentAudioSource = audioManager.GetAudioSource("Ambience", "SeaWaves");
        yield return FadeAudioSource.StartFade(currentAudioSource, 0.3f, 0f);
        currentAudioSource = audioManager.GetAudioSource("Ambience", "Abyss");
        currentAudioSource.Play();
        yield return FadeAudioSource.StartFade(currentAudioSource, 0.3f, 1f);
        yield return SmoothLerp(time);
        animator.SetBool("isUnderwater", true);
        boxCollider2D.enabled = true;
    }

    private IEnumerator WaitForSoundToFinish(AudioSource audioSource)
    {
        yield return new WaitForSeconds(audioSource.clip.length);
    }
}