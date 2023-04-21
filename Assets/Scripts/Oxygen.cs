using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oxygen : MonoBehaviour
{
    [SerializeField] private float maxOxygen;
    private GameObject bar;

    // Start is called before the first frame update
    void Start()
    {
        bar = this.gameObject;
    }

    public void AnimateBar()
    {
        ResetBar();
        if (GameManager.GetInstance().IsPlayerUnderwater())
        {
            LeanTween.scaleX(bar, 0, maxOxygen).setOnComplete(EndGame);
        }
    }

    public void ResetBar()
    {
        LeanTween.cancel(bar);
        LeanTween.scaleX(bar, 1, 0);
    }

    public void EndGame()
    {
        ResetBar();
        GameManager.GetInstance().EndGame();
    }


}