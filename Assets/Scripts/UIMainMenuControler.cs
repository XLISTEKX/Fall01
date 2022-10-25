using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMainMenuControler : MonoBehaviour
{
    public GameObject rightOffset;
    public GameObject leftOffset;
    public GameObject middle;

    public GameObject[] menuStates;
    short currentState = 0;

    GameObject currentMenuState;

    private void Start()
    {
        currentMenuState = Instantiate(menuStates[0], transform);
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

            currentMenuState = Instantiate(menuStates[currentState],transform);
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

            currentMenuState = Instantiate(menuStates[currentState], transform);
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
    
}
