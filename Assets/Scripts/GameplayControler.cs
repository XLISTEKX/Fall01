using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayControler : MonoBehaviour
{
    [Header("SpawnRates")]
    public uint cycle = 0;

    public PlayerGamePlayControler playerGamePlayControler;
    public UIControler uiControler;
    public GameObject[] particle;
    public float[] spawnChance;
    public Vector2 spawnOffset;

    public float spawnRate;
    public bool isDead;
    public uint score;

    private void Start()
    {
        InvokeRepeating("spawnObject", spawnRate, spawnRate);
        InvokeRepeating("addScore", 5f, 3f);
    }

    private void Update()
    {
        if (isDead && Time.timeScale != 0)
        {
            CancelInvoke("addScore");
            Time.timeScale = Mathf.Lerp(Time.timeScale, 0, 1.25f * Time.deltaTime);
            if (Time.timeScale < 0.15f)
            {
                Time.timeScale = 0;

                uiControler.openDeadScreen();
            }
                
        }
    }

    void spawnObject()
    {
        short xSpawnValue = (short) Random.Range(-playerGamePlayControler.gameplaySize.x, playerGamePlayControler.gameplaySize.x);

        float x = Random.value;
        if(x < spawnChance[1])
        {
            Instantiate(particle[1], new Vector2(xSpawnValue, 0) + spawnOffset, transform.rotation);
        }
        else 
        {
            Instantiate(particle[0], new Vector2(xSpawnValue, 0) + spawnOffset, transform.rotation);

        }
    }

    void addScore()
    {
        score++;
        uiControler.updateScore(score);
        
        if(score % 10 == 0)
        {
            CancelInvoke("spawnObject");
            spawnRate *= 0.8f;
            cycle++;
            InvokeRepeating("spawnObject", 0, spawnRate);
        }
    }
}
