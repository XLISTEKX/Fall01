using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvent_First : GameEventPrefab
{
    public GameObject minesweeper;
    public GameObject hackPrefabUI;
    public int hackTime = 20;

    public HackUI hackUI;
    public override void doGameEvent()
    {
        
        hackUI.hackTime = hackTime;
        hackUI.gameEvent = this;
        hackUI.activeHack();
    }
}
