using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIControler : MonoBehaviour
{
    
    public TMP_Text score;
    [Header("DeadScreen")]
    public TMP_Text scoreDeadScreen;
    public TMP_Text highscoreDeadScreen;
    public GameObject newBest;
    [Header("UI")]
    public GameObject[] Screens; //0 - Gameplay, 1 - DeadScreen

    uint currentScore;

    public void updateScore(uint currentScore)
    {
        this.currentScore = currentScore;
        score.text = currentScore.ToString();
    }

    public void openDeadScreen()
    {
        Screens[0].SetActive(false);
        Screens[1].SetActive(true);
        scoreDeadScreen.text = "Score: " + currentScore.ToString();

        if (PlayerPrefs.GetInt("HighScore") < currentScore)
        {
            newBest.SetActive(true);
            PlayerPrefs.SetInt("HighScore", (int) currentScore) ;
            PlayerPrefs.Save();
        }
        highscoreDeadScreen.text = "High Score: " + PlayerPrefs.GetInt("HighScore").ToString();
        
        
    }

    public void playAgainButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

}
