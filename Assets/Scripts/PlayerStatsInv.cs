using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsInv : MonoBehaviour
{
    [Header("Inventory")]
    public uint money;
    public List<GameObject> skins;
    public GameObject currentSkin;

    private void Start()
    {
        currentSkin = skins[0];
        money = (uint) PlayerPrefs.GetInt("Money");
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
}
