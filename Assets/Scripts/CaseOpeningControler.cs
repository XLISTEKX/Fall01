
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CaseOpeningControler : MonoBehaviour
{
    [Header("Settings")]
    public float openingTime;
    public int slotsAmount;
    public int caseCost;

    [Header("Case Content")]
    public List<GameObject> caseContent;
    public List<float> caseChance;


    [Header("Case Offsets")]
    public GameObject slotSpawnOffset;
    public GameObject dropView;
    public TMP_Text dropedMoneyTXT;
    public TMP_Text caseCostTXT;

    List<GameObject> spawnedSlots = new List<GameObject>();
    bool moveContent;
    bool isOpening;
    PlayerStatsInv playerStats;
    Vector2 destination;
    int caseIndexToSpawn;


    private void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("EventSystem").GetComponent<PlayerStatsInv>();
        caseCostTXT.text = caseCost.ToString();
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

            for (int i = 0; i < slotsAmount; i++)
            {
                float chance = Random.value;

                for (int j = 0; j < caseChance.Count; j++)
                {
                    if (chance <= caseChance[j])
                    {
                        caseIndexToSpawn = j;
                        break;
                    }
                }
                spawnedSlots.Add(Instantiate(caseContent[caseIndexToSpawn], slotSpawnOffset.transform));



            }

            Invoke("checkDes", 0.2f);

        


    }
    void checkDes()
    {
        float temp = -spawnedSlots[slotsAmount - 3].transform.position.x;
        destination = new Vector2(Random.Range(temp -1.25f, temp + 1.25f), spawnedSlots[slotsAmount - 3].transform.position.y);
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
            int dropedMoney = spawnedSlots[slotsAmount - 3].GetComponent<CaseOpeningSlots>().dropedAmount;

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
}
