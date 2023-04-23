using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oxygen : MonoBehaviour
{
    [SerializeField] private float maxOxygen;
    [SerializeField] private float timeToRecharge;
    private GameObject bar;

    // Start is called before the first frame update
    void Start()
    {
        bar = this.gameObject;
    }

    public void DecreaseOxygen()
    {
        LeanTween.scaleX(bar, 0, maxOxygen).setOnComplete(EndGame);
    }

    public void RechargeOxygen()
    {
        LeanTween.cancel(bar);
        LeanTween.scaleX(bar, 1, timeToRecharge);
    }

    public void EndGame()
    {
        GameManager.GetInstance().EndGame();
    }
}