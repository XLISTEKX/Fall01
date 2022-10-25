using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIControler : MonoBehaviour
{
    public TMP_Text score;
    public TMP_Text scoreDeadScreen;

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
        scoreDeadScreen.text = "Score: " + currentScore.ToString();
        Screens[1].SetActive(true);
    }

    public void playAgainButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

}
