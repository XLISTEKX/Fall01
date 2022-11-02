using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopControler : MonoBehaviour
{
    public List<GameObject> skinsToBuy;
    PlayerStatsInv playerStats;

    private void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("EventSystem").GetComponent<PlayerStatsInv>();
    }

    public void buySkin(int skinIndex)
    {
        playerStats.skins.Add(skinsToBuy[skinIndex]);
    }
}
