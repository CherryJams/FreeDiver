using System;
using UnityEngine;

public class Surface : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private PlayerController playerController;

    private void Awake()
    {
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "Player") ;
        {
            Debug.Log("Player is on surface");
            playerController.SetIsUnderwater(false);
            animator.SetBool("isUnderwater", false);
            AudioManager.GetInstance().GetAudioSource("Ambience", "Abyss").Stop();
            AudioManager.GetInstance().GetAudioSource("Ambience", "SeaWaves").Play();
        }
    }
}