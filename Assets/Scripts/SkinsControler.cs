using UnityEngine;
using TMPro;

public class SkinsControler : MonoBehaviour
{
    public GameObject skinSpawnOffset;
    public TMP_Text skinNameTXT;

    PlayerStatsInv playerStats;
    GameObject currentObjectSkin;

    private void Start()
    {
        playerStats = gameObject.GetComponent<PlayerStatsInv>();
        setSkin();
        
    }
    public void setSkin()
    {
        if(currentObjectSkin != null)
        {
            Destroy(currentObjectSkin);
        }
        

        currentObjectSkin = Instantiate(playerStats.currentSkin, skinSpawnOffset.transform);
        currentObjectSkin.transform.localScale = new Vector3(1.2f, 1.2f, 1);
    }

}
