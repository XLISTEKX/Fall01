using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameEventControler : MonoBehaviour
{
    [Header("Settings")]
    public float lookTime;
    public int eventIndex;
    public List<float> eventChances;
    public GameEventPrefab currentEvent; // 0 - Minesweeper
    public GameObject hackImage;
    public GameObject minesweeper;

    private void Start()
    {
        InvokeRepeating("lookForEvent", lookTime, lookTime);
    }

    void lookForEvent()
    {
        float randomChance = Random.value;
        for (int i = 0; i < eventChances.Count; i++)
        {
            if(randomChance <= eventChances[i])
            {
                setNewGameEvent(i);
                break;
            }
        }
        CancelInvoke("lookForEvent");
    }
    public void setNewGameEvent(int index)
    {
        eventIndex = index;
        switch (eventIndex)
        {
            case 0:
                HackUI hackUI = Instantiate(hackImage, GameObject.FindGameObjectWithTag("HackUI").transform).GetComponent<HackUI>();
                currentEvent = gameObject.AddComponent<GameEvent_First>();
                currentEvent.GetComponent<GameEvent_First>().hackUI = hackUI;
                currentEvent.GetComponent<GameEvent_First>().minesweeper = minesweeper;
                currentEvent.doGameEvent();
                break;
        }


    }
    
}
