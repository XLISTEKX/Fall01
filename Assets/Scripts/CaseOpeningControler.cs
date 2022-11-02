
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CaseOpeningControler : MonoBehaviour
{
    [Header("Settings")]
    public int caseIndex;
    public float openingTime;
    public int slotsAmount;
    public int caseCost;

    [Header("Case Content")]
    public List<GameObject> caseContent;
    public List<float> caseChance;
    public List<Chest> allChests;


    [Header("Case Offsets")]
    public GameObject slotSpawnOffset;
    public GameObject dropView;
    public GameObject chanceView;
    public TMP_Text dropedMoneyTXT;
    public TMP_Text caseCostTXT;
    public TMP_Text caseNameTXT;
    public TMP_Text unrareTXT;
    public TMP_Text rareTXT;
    public TMP_Text mythicTXT;
    public TMP_Text legenderyTXT;

    List<GameObject> spawnedSlots = new List<GameObject>();
    bool moveContent;
    int rolledSlotID;
    bool isOpening;
    PlayerStatsInv playerStats;
    Vector2 destination;
    int caseIndexToSpawn;


    private void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("EventSystem").GetComponent<PlayerStatsInv>();
    }


    private void Update()
    {
        if (moveContent)
        {
            
            slotSpawnOffset.transform.position = Vector2.Lerp(slotSpawnOffset.transform.position, destination, Time.deltaTime * openingTime);

            if(slotSpawnOffset.transform.position.x <= destination.x + 0.25f)
            {
                moveContent = false;

                openDropView(true);
                isOpening = false;

            }
        }
    }
    public void setCase(int caseIndex)
    {

        this.caseIndex = caseIndex;

        caseCost = allChests[caseIndex].chestCost;
        caseCostTXT.text = caseCost.ToString();

        caseChance = allChests[caseIndex].chestChanses;
        caseNameTXT.text = allChests[caseIndex].chestName;
    }




    public void openCase()
    {
        if (isOpening)
        {
            return;
        }
        if(caseCost > playerStats.money)
        {
            return;
        }
        playerStats.money -= (uint) caseCost;
        playerStats.saveStats();
        isOpening = true;
        resetCase();

        //Loop through all Slots
        for (int i = 0; i < slotsAmount; i++)
        {
            float chance = Random.value;

            //Get chance for slot
            for (int j = 0; j < caseChance.Count; j++)
            {
                if (chance <= caseChance[j])
                {
                    caseIndexToSpawn = j;
                    break;
                }
            }
            int index;
            switch (caseIndexToSpawn)
            {
                case 0:
                    index = Random.Range(0, allChests[caseIndex].Unrare.Count);
                    spawnedSlots.Add(Instantiate(allChests[caseIndex].Unrare[index], slotSpawnOffset.transform));
                    break;
                case 1:
                    index = Random.Range(0, allChests[caseIndex].Rare.Count);
                    spawnedSlots.Add(Instantiate(allChests[caseIndex].Rare[index], slotSpawnOffset.transform));
                    break;
                case 2:
                    index = Random.Range(0, allChests[caseIndex].Mythic.Count);
                    spawnedSlots.Add(Instantiate(allChests[caseIndex].Mythic[index], slotSpawnOffset.transform));
                    break;
                case 3:
                    index = Random.Range(0, allChests[caseIndex].Legendery.Count);
                    spawnedSlots.Add(Instantiate(allChests[caseIndex].Legendery[index], slotSpawnOffset.transform));
                    break;
            }
            



        }
        rolledSlotID = Random.Range(slotsAmount - 7, slotsAmount - 4);
        Invoke("checkDes", 0.2f);




    }
    void checkDes()
    {
        float temp = -spawnedSlots[rolledSlotID].transform.position.x;
        destination = new Vector2(Random.Range(temp -1.25f, temp + 1.25f), spawnedSlots[rolledSlotID].transform.position.y);
        moveContent = true;
    }

    void resetCase()
    {
        moveContent = false;
        if(spawnedSlots.Count > 0)
        {
            foreach (GameObject item in spawnedSlots)
            {
                Destroy(item);
            }
        }
        
        spawnedSlots.Clear();
        slotSpawnOffset.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        
    }

    public void openDropView(bool open)
    {
        if (open)
        {
            dropView.SetActive(true);
            int dropedMoney = spawnedSlots[rolledSlotID ].GetComponent<CaseOpeningSlots>().dropedAmount;

            playerStats.money += (uint) dropedMoney;
            playerStats.saveStats();
            dropedMoneyTXT.text = dropedMoney.ToString();
            
        }
        else
        {
            resetCase();
            dropView.SetActive(false);
        }
    }
    public void openChnceView(bool open)
    {
        if (open)
        {
            chanceView.SetActive(true);
            updateChances();

        }
        else
        {
            chanceView.SetActive(false);
        }
    }
    void updateChances()
    {
        unrareTXT.text = "Unrare: " + (caseChance[0] * 100).ToString() + "%";
        rareTXT.text = "Rare: " + ((caseChance[1] - caseChance[0]) * 100).ToString() + "%";
        mythicTXT.text = "Mythic: " + ((caseChance[2] - caseChance[1]) * 100).ToString() + "%";
        legenderyTXT.text = "Legendery: " + ((caseChance[3] -caseChance[2]) * 100).ToString() + "%";
    }
}
