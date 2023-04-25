using System;
using TMPro;
using UnityEngine;

public class DepthGauge : MonoBehaviour
{
    [SerializeField] GameObject player;
    private int depth;
    private int maxDepth;
    [SerializeField] private TextMeshProUGUI depthCounter;

    private void Awake()
    {
        
    }

    private void Update()
    {
        depth = (int) player.transform.position.y * -1;
        depthCounter.text = depth.ToString() + "m";
        if (depth > maxDepth)
        {
            maxDepth = depth;
        }
    }

    public int GetDepth()
    {
        return depth;
    }

    public int GetMaxDepth()
    {
        return maxDepth;
    }
}