using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestControler : MonoBehaviour
{
    public RectTransform questPanel;
    public Button questButton;
    public float openSpeed;

    bool open = true;

    Vector2 destination;
    Vector2 offset = new Vector2(-225, 0);


    private void Start()
    {
        destination = questPanel.anchoredPosition;
    }

    void Update()
    {
        questPanel.anchoredPosition = Vector2.Lerp(questPanel.anchoredPosition, destination, Time.deltaTime * openSpeed);

    }

    public void openQuests()
    {
        if (open)
        {
            destination = offset;
            open = false;

        }
        else
        {
            open = true;
            destination = -offset;
        }

    }

}
