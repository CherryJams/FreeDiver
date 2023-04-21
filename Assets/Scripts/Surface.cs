using System;
using System.Collections;
using UnityEngine;

public class Surface : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private PlayerController playerController;
    private AudioManager audioManager;
    private AudioSource currentAudioSource;
    private void Awake()
    {
        audioManager = AudioManager.GetInstance();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "Player") ;
        {
            StartCoroutine(Rise(other));
        }
    }

    private IEnumerator Rise(Collision2D other)
    {
        Debug.Log("Player is on surface");
        playerController.SetIsUnderwater(false);
        playerController.SetCanMoveVertically(false);
        animator.SetBool("isUnderwater", false);
        other.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        currentAudioSource=audioManager.GetAudioSource("Ambience", "Abyss");
        yield return FadeAudioSource.StartFade(currentAudioSource, 0.3f, 0f);
        currentAudioSource = audioManager.GetAudioSource("Ambience", "SeaWaves");
        yield return FadeAudioSource.StartFade(currentAudioSource, 0.3f, 1f);

    }
}