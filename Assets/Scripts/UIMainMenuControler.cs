using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.VisualScripting;

public class UIMainMenuControler : MonoBehaviour
{
    [Header("Offsets")]
    public RectTransform rightOffset;
    public RectTransform leftOffset;
    public RectTransform middle;

    [Header("Canvas")]
    public GameObject SettingsTab;
    public GameObject MainMenuTab;
    public GameObject UpgradesTab;
    public GameObject SkinsTab;
    public GameObject ShopTab;
    public GameObject CaseTab;

    [Header("Settings")]
    public TMP_Text currentQualityTxt;
    public TMP_Text currentVolumeTxt;
    public Slider volumeSlider;

    [Header("Main Menu")]
    public TMP_Text moneyTXT;
    public float cameraMoveSpeed;
    public CaseOpeningControler caseOpeningControl;

    [Header("Shop Menu")]
    public TMP_Text shopMoneyTXT;

    [Header("Dependencies")]
    public GameObject[] menuStates;
    PlayerStatsInv playerStats;
    short currentState = 0;

    GameObject currentMenuState;
    bool moveCamera;
    Vector3 destinationUpgrade;
    Vector3 destination;

    private void Start()
    {
        
        playerStats = gameObject.GetComponent<PlayerStatsInv>();
        playerStats.updateStats();

        destinationUpgrade = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 4, Screen.height / 2));

        currentMenuState = Instantiate(menuStates[0], middle.transform);
        currentMenuState.GetComponent<MainMenuStatesControler>().desVector = middle.transform.position;
        updateMenu();
    }

    private void Update()
    {
        if (moveCamera)
        {
            Camera.main.gameObject.transform.position = Vector2.Lerp(Camera.main.gameObject.transform.position, destination, Time.deltaTime * cameraMoveSpeed);
        }
    }
    public void updateMenu()
    {
        moneyTXT.text = playerStats.money.ToString();
    }


    public void changeMenuState(int state)
    {
        MainMenuStatesControler mainMenuStatesControler = currentMenuState.GetComponent<MainMenuStatesControler>();


        if(state > 0)
        {
            if(menuStates.Length == currentState + 1)
            {
                currentState = 0;
            }
            else
            {
                currentState++;
            }
            mainMenuStatesControler.desVector = leftOffset.anchoredPosition;
            mainMenuStatesControler.invokerDestroy();

            currentMenuState = Instantiate(menuStates[currentState], middle.transform);
            currentMenuState.transform.position = rightOffset.position;
            currentMenuState.GetComponent<MainMenuStatesControler>().desVector = middle.anchoredPosition;
        }
        else
        {
            if (currentState == 0)
            {
                currentState = (short) (menuStates.Length - 1);
            }
            else
            {
                currentState--;
            }
            mainMenuStatesControler.desVector = rightOffset.anchoredPosition;
            mainMenuStatesControler.invokerDestroy();

            currentMenuState = Instantiate(menuStates[currentState], middle.transform);
            currentMenuState.transform.position = leftOffset.position;
            currentMenuState.GetComponent<MainMenuStatesControler>().desVector = middle.anchoredPosition;
        }
        
    }

    public void openCurrentState()
    {
        switch (currentState)
        {
            case 0:
                {
                    GameObject.FindGameObjectWithTag("GameSettings").GetComponent<GameSettingsControler>().playerSkin = playerStats.currentSkin;
                    SceneManager.LoadScene(1);

                    return;
                }
            case 1:
                {
                    openUpgrades(true);

                    return;

                }

            case 2:
                {
                    openSkins(true);

                    return;
                }
            case 3:
                {
                    openShop(true);

                    return;
                }
            case 4:
                {
                    SceneManager.LoadScene(2);
                    return;
                }
        }
    }
    /*---------------------------------------------------------------------------------
                                SETTINGS TAB
     ----------------------------------------------------------------------------------
     
     */
    public void openSettings(bool open)
    {
        if (open)
        {
            SettingsTab.SetActive(true);
            MainMenuTab.SetActive(false);

            updateSettings();
        }
        else
        {
            SettingsTab.SetActive(false);
            MainMenuTab.SetActive(true);
        }
    }

    public void updateSettings()
    {
        currentQualityTxt.text = QualitySettings.names[QualitySettings.GetQualityLevel()].ToUpper();
    }

    public void changeGraphicState()
    {
        short qualityLevel = (short) QualitySettings.GetQualityLevel();

        if (QualitySettings.names.Length > qualityLevel + 1)
        {
            qualityLevel++;
        }
        else
        {
            qualityLevel = 0;
        }
        QualitySettings.SetQualityLevel(qualityLevel, true);
        updateSettings();
    }


    public void updateVolumeState()
    {
        currentVolumeTxt.text = volumeSlider.value.ToString() + "%";

    }

    /*---------------------------------------------------------------------------------
                                UPGRADES TAB
     ----------------------------------------------------------------------------------
     */

    public void openUpgrades(bool open)
    {
        if (open)
        {
            UpgradesTab.SetActive(true);
            MainMenuTab.SetActive(false);


            destination = destinationUpgrade;

            moveCamera = true;

            

        }
        else
        {
            UpgradesTab.SetActive(false);
            MainMenuTab.SetActive(true);
            destination = new Vector3(0, 0, 0);
        }
    }

    /*---------------------------------------------------------------------------------
                                SKINS TAB
     ----------------------------------------------------------------------------------
     */

    public void openSkins(bool open)
    {
        if (open)
        {
            SkinsTab.SetActive(true);
            MainMenuTab.SetActive(false);

        }
        else
        {
            SkinsTab.SetActive(false);
            MainMenuTab.SetActive(true);
        }
    }

    /*---------------------------------------------------------------------------------
                                Shop TAB
     ----------------------------------------------------------------------------------
     
     */
    public void openShop(bool open)
    {
        if (open)
        {
            ShopTab.SetActive(true);
            MainMenuTab.SetActive(false);
            updateShop();
            

        }
        else
        {
            ShopTab.SetActive(false);
            MainMenuTab.SetActive(true);
            updateMenu();
        }
    }

    public void updateShop()
    {
        shopMoneyTXT.text = playerStats.money.ToString();
    }

    public void openCase(int CaseIndex)
    {
        if (CaseIndex >= 0)
        {
            caseOpeningControl.setCase(CaseIndex);

            CaseTab.SetActive(true);
            ShopTab.SetActive(false);

        }
        else
        {
            updateShop();
            CaseTab.SetActive(false);
            ShopTab.SetActive(true);
        }
    }

}
