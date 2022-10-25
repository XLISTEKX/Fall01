using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGamePlayControler : MonoBehaviour
{
    public float playerMoveSpeed;
    public float screenOffset;

    public Vector2 gameScreenSize;
    public Vector2 gameplaySize;

    public GameObject gamePlayer;
    public bool isDead;

    void Start()
    {
        gameScreenSize = new Vector2(Screen.width, Screen.height);

        gameplaySize = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        
    }


    void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            if(Input.mousePosition.x > gameScreenSize.x / 2)
            {
                moveCharacter(1);
            }
            else 
            {
                moveCharacter(-1);
            }
        }   
    }
    private void Update()
    {
        if (isDead && Time.timeScale != 0)
        {
            Time.timeScale = Mathf.Lerp(Time.timeScale, 0, 1f * Time.deltaTime);
            if (Time.timeScale < 0.15f)
                Time.timeScale = 0;
        }
    }
    void moveCharacter(int direction)
    {
        float xValue;
        xValue = Mathf.Clamp(gamePlayer.transform.position.x + direction * playerMoveSpeed * Time.deltaTime, -gameplaySize.x +screenOffset, gameplaySize.x - screenOffset);
        gamePlayer.transform.position = new Vector2(xValue, gamePlayer.transform.position.y);
    }

}
