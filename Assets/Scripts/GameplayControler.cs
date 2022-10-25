using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayControler : MonoBehaviour
{
    public PlayerGamePlayControler playerGamePlayControler;
    public GameObject[] particle;
    public float[] spawnChance;
    public Vector2 spawnOffset;

    public float spawnRate;

    private void Start()
    {
        InvokeRepeating("spawnObject", spawnRate, spawnRate);
    }

    void spawnObject()
    {
        short xSpawnValue = (short) Random.Range(-playerGamePlayControler.gameplaySize.x, playerGamePlayControler.gameplaySize.x);

        if(Random.value < spawnChance[1])
        {
            Instantiate(particle[1], new Vector2(xSpawnValue, 0) + spawnOffset, transform.rotation);
        }
        else 
        {
            Instantiate(particle[0], new Vector2(xSpawnValue, 0) + spawnOffset, transform.rotation);

        }
    }
}
