using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HackUI : MonoBehaviour
{
    public int hackTime;
    public Slider hackTimer;
    public GameEvent_First gameEvent;
    
    public void activeHack()
    {
        hackTimer.maxValue = hackTime;
        hackTimer.value = hackTime;


        InvokeRepeating("updateTimer", 1, 1);
    }

    void updateTimer()
    {
        hackTime--;
        if (hackTime <= 0) CancelInvoke("updateTimer");
        hackTimer.value = hackTime;
    }

    public void openHack()
    {
        Time.timeScale = 0;
        Instantiate(gameEvent.minesweeper, GameObject.FindGameObjectWithTag("UIControler").transform);
    }
}
