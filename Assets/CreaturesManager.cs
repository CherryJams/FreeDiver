using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

public class CreaturesManager : MonoBehaviour
{
    [SerializeField] private GameObject creature;
    [SerializeField] private Vector2 spawnArea;
    [SerializeField] private float spawnTimer;
    [SerializeField] private Transform player;
    private float timer;

    private void Update()
    {
        timer-=Time.deltaTime;
        if (timer < 0f)
        {
            SpawnCreature();
            timer = spawnTimer;
        }
    }

    private void SpawnCreature()
    {
       Vector3 position= GenerateRandomPosition();

       position += player.transform.position;
       GameObject newCreature = Instantiate(creature);
       newCreature.transform.position = position;
    }

    private Vector3 GenerateRandomPosition()
    {
        Vector3 position = new Vector3();
        
        float f =UnityEngine.Random.value>0.5f?1:-1;
        if (UnityEngine.Random.value > 0.5f)
        {
            position.x = UnityEngine.Random.Range(-spawnArea.x, spawnArea.x);
            position.y = f * spawnArea.y;
        }
        else
        {
            position.y=UnityEngine.Random.Range(-spawnArea.y, spawnArea.y);
            position.x = f * spawnArea.x;
        }

        position.z = 0;
        return position;
    }
}
