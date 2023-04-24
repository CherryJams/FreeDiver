using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

public class CreaturesManager : MonoBehaviour
{
    [SerializeField] private GameObject creature;
    [SerializeField] private Vector2 spawnArea;
    [SerializeField] private float spawnRepeatRate;
    [SerializeField] private float firstSpawnDelay;
    [SerializeField] private Transform player;
    [SerializeField] private DepthGauge depthGauge;
    public static CreaturesManager instance;
    [SerializeField] private List<GameObject>[] creaturesLists;
    [SerializeField] private GameObject[] species;
    [SerializeField] private int amountToPool;
    private Vector3 randomPos;
    private Vector3 playerPos;
    private int playerDepth;
    private GameObject newCreature;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        creaturesLists = new List<GameObject>[species.Length];
        for (int i = 0; i < this.species.Length; i++)
        {
            var speciesGroup = new List<GameObject>();
            for (int j = 0; j < amountToPool; j++)
            {
                GameObject creature = Instantiate(species[i]);
                creature.SetActive(false);
                speciesGroup.Add(creature);
            }

            creaturesLists[i] = speciesGroup;
        }
        
        InvokeRepeating("SpawnCreature", firstSpawnDelay, spawnRepeatRate);
    }

    public GameObject GetPooledObject(int speciesIndex)
    {
        for (int i = 0; i < creaturesLists[speciesIndex].Count; i++)
        {
            if (!creaturesLists[speciesIndex][i].activeInHierarchy)
            {
                return creaturesLists[speciesIndex][i];
            }
        }

        return null;
    }


    private void SpawnCreature()
    {
        randomPos = GenerateRandomPosition();
        playerPos = player.transform.position;
        randomPos += playerPos;
        playerDepth = depthGauge.GetDepth();
        if (playerDepth >= 15 && playerDepth <= 60)
        {
            newCreature = GetPooledObject(0);
        }
        if (playerDepth >= 70 && playerDepth <= 120)
        {
            newCreature = GetPooledObject(1);
        }
        if (playerDepth >= 130 && playerDepth <= 180)
        {
            newCreature = GetPooledObject(2);
        }
        if (playerDepth >= 230 && playerDepth <= 280)
        {
            newCreature = GetPooledObject(3);
        }
        if (playerDepth >= 320 && playerDepth <= 370)
        {
            newCreature = GetPooledObject(4);
        }
        if (playerDepth >= 420 )
        {
            newCreature = GetPooledObject(5);
        }

        if (newCreature != null)
        {
            newCreature.SetActive(true);
            newCreature.transform.position = randomPos;
            newCreature.transform.SetParent(gameObject.transform);
        }
    }

    private Vector3 GenerateRandomPosition()
    {
        Vector3 position = new Vector3();

        float f = UnityEngine.Random.value > 0.5f ? 1 : -1;
        if (UnityEngine.Random.value > 0.5f)
        {
            position.x = UnityEngine.Random.Range(-spawnArea.x, spawnArea.x);
            position.y = f * spawnArea.y;
        }
        else
        {
            position.y = UnityEngine.Random.Range(-spawnArea.y, spawnArea.y);
            position.x = f * spawnArea.x;
        }

        position.z = 0;
        return position;
    }
    public void IncreaseSpawnRate()
    {
        var temp = spawnRepeatRate;
        temp /= 100;
        temp *= 5;
        spawnRepeatRate -= temp;
    }

}