using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIMainMenuControler : MonoBehaviour
{
    [Header("Offsets")]
    public GameObject rightOffset;
    public GameObject leftOffset;
    public GameObject middle;

    [Header("Canvas")]
    public GameObject SettingsTab;
    public GameObject MainMenuTab;

    [Header("Settings")]
    public TMP_Text currentQualityTxt;
    public TMP_Text currentVolumeTxt;
    public Slider volumeSlider;

    [Header("Main Menu")]
    public TMP_Text moneyTXT;

    [Header("Dependencies")]
    public GameObject[] menuStates;
    PlayerStatsInv playerStats;
    short currentState = 0;

    GameObject currentMenuState;

    private void Start()
    {
        playerStats = gameObject.GetComponent<PlayerStatsInv>();

        moneyTXT.text = playerStats.money.ToString();

        currentMenuState = Instantiate(menuStates[0], middle.transform);
        currentMenuState.GetComponent<MainMenuStatesControler>().desVector = middle.transform.position;
    }


    public void changeMenuState(int state)
    {

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
            currentMenuState.GetComponent<MainMenuStatesControler>().desVector = leftOffset.transform.position;
            currentMenuState.GetComponent<MainMenuStatesControler>().invokerDestroy();

            currentMenuState = Instantiate(menuStates[currentState], middle.transform);
            currentMenuState.transform.position = rightOffset.transform.position;
            currentMenuState.GetComponent<MainMenuStatesControler>().desVector = middle.transform.position;
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
            currentMenuState.GetComponent<MainMenuStatesControler>().desVector = rightOffset.transform.position;
            currentMenuState.GetComponent<MainMenuStatesControler>().invokerDestroy();

            currentMenuState = Instantiate(menuStates[currentState], middle.transform);
            currentMenuState.transform.position = leftOffset.transform.position;
            currentMenuState.GetComponent<MainMenuStatesControler>().desVector = middle.transform.position;
        }
        
    }

    public void openCurrentState()
    {
        switch (currentState)
        {
            case 0:
                {
                    SceneManager.LoadScene(1);

                    return;
                }
            case 1:
                {


                    return;

                }

            case 2:
                {
                    Application.Quit();

                    return;
                }
        }
    }

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
}
