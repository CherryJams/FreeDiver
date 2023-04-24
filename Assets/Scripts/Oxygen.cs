using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oxygen : MonoBehaviour
{
    [SerializeField] private float maxOxygen;
    [SerializeField] private float timeToRecharge;
    [SerializeField] private GameObject player;
    private GameObject bar;

    // Start is called before the first frame update
    void Start()
    {
        bar = this.gameObject;
    }

    private void Update()
    {
        bar.transform.position = player.transform.position + new Vector3(-0.5f, -0.8f, 0);
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

    public void IncreaseOxygen()
    {
        var temp = maxOxygen;
        temp /= 100;
        temp *= 5;
        maxOxygen += temp;
    }
}