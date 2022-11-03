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

    private void Start()
    {
        currentSkin = skins[0];
        currentSkinID = 0;
        money = (uint) PlayerPrefs.GetInt("Money");

        //GetComponent<SkinsControler>().setSkin();

        Debug.Log(money + "wczytane");
    }

    public void updateStats()
    {
        money = (uint)PlayerPrefs.GetInt("Money");
    }

    public void saveStats()
    {
        PlayerPrefs.SetInt("Money", (int) money);
        Debug.Log(money + "zapisane");
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
