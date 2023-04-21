using System;
using UnityEngine;

public class DepthGauge : MonoBehaviour
{
    private int depth;

    private void Update()
    {
        depth = (int) gameObject.transform.position.y;
    }
    public int GetDepth()
    {
        return depth;
    }
}