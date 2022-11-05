using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventControler : MonoBehaviour
{
    public int eventIndex;

    public GameEventPrefab currentEvent; // 0 - 
    public void setNewGameEvent(int index)
    {
        eventIndex = index;
        switch (eventIndex)
        {
            case 0:
                currentEvent = gameObject.AddComponent<GameEvent_First>();
                currentEvent.doGameEvent();
                break;
        }


    }
    
}
