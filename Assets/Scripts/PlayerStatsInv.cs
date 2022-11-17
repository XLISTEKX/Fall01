using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsInv : MonoBehaviour
{
    [Header("Inventory")]
    public uint money;
    public List<GameObject> skins;
    public GameObject currentSkin;
    public int currentSkinID;

    [Header("Player Stats")]
    public uint allScore;
    public uint moneyGainded;
    public uint mineSweeperWon;

    private void Start()
    {
        currentSkin = skins[0];
        currentSkinID = 0;
        money = (uint) PlayerPrefs.GetInt("Money");

        //GetComponent<SkinsControler>().setSkin();

    }

    public void updateStats()
    {
        money = (uint)PlayerPrefs.GetInt("Money");
        allScore=(uint)PlayerPrefs.GetInt("allScore");
        mineSweeperWon = (uint)PlayerPrefs.GetInt("MSWon");
        moneyGainded = (uint)PlayerPrefs.GetInt("MoneyOverall");
    }

    public void saveStats()
    {
        long gained = (int) money - PlayerPrefs.GetInt("Money");
        if (gained > 0)
        {
            moneyGainded += (uint)gained;
        }

        PlayerPrefs.SetInt("Money", (int) money);
        PlayerPrefs.SetInt("allScore", (int)allScore);
        PlayerPrefs.SetInt("MSWon", (int)mineSweeperWon);
        PlayerPrefs.SetInt("MoneyOverall", (int)moneyGainded);
        PlayerPrefs.Save();
    }


    public void changeSkin(int x)
    {
        if(x > 0)
        {
            if(skins.Count > currentSkinID + x)
            {
                currentSkinID += x;
                
            }
            else
            {
                currentSkinID = 0;
            }
        }
        else if (x < 0)
        {
            if (currentSkinID + x < 0)
            {
                currentSkinID = skins.Count - 1;

            }
            else
            {
                currentSkinID += x;
            }
            

        }

        currentSkin = skins[currentSkinID];
        GetComponent<SkinsControler>().setSkin();

    }
}
