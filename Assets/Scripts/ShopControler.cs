using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopControler : MonoBehaviour
{
    [Header("UIOffsets")]
    public TMP_Text buyNameTXT;
    public TMP_Text buyCostTXT;
    [Header("Settings")]
    public GameObject buyPanel;
    public Image skinModel;
    public List<GameObject> skinsToBuy;
    PlayerStatsInv playerStats;
    int index;
    private void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("EventSystem").GetComponent<PlayerStatsInv>();
    }


    public void chooseToBuy(int skinIndex)
    {
        buyPanel.SetActive(true);
        index = skinIndex;
        updateBuyPanel();
    }
    public void updateBuyPanel()
    {
        SkinSettings skinSettings = skinsToBuy[index].GetComponent<SkinSettings>();
        buyNameTXT.text = skinSettings.skinName;
        buyNameTXT.color = skinSettings.rarityColor;
        buyCostTXT.text = skinSettings.skinCost.ToString();
        skinModel.sprite = skinsToBuy[index].GetComponent<SpriteRenderer>().sprite;
        
    }

    public void buySkin()
    {
        int cost = skinsToBuy[index].GetComponent<SkinSettings>().skinCost;
        if (playerStats.money >= cost)
        {
            playerStats.money -= (uint)cost;
            playerStats.saveStats();
            playerStats.skins.Add(skinsToBuy[index]);
        }
        GameObject.FindGameObjectWithTag("EventSystem").GetComponent<UIMainMenuControler>().updateShop();
        
    }
}
