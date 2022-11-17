using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayControler : MonoBehaviour
{
    [Header("SpawnRates")]
    public uint cycle = 0;
    public float spawnSpeedingRate;
    public float maxSpeedRate;
    public float minSpeedRate;
    float spawnRate;
    
    [Header("Dependencies")]
    public PlayerGamePlayControler playerGamePlayControler;
    public UIControler uiControler;
    public GameObject[] particle;
    public float[] spawnChance;
    public Vector2 spawnOffset;

    
    public bool isDead;
    public uint score;

    private void Start()
    {
        Invoke("spawnObject", spawnRate);
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
        for(int i = 0; i < particle.Length; i++)
        {
            if(x <= spawnChance[i])
            {
                Instantiate(particle[i], new Vector2(xSpawnValue, 0) + spawnOffset, transform.rotation);
                break;
            }
        }


        spawnRate = (maxSpeedRate - minSpeedRate) * Mathf.Pow(spawnSpeedingRate, -score) + minSpeedRate;

        CancelInvoke("spawnObject");
        Invoke("spawnObject", spawnRate);
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
