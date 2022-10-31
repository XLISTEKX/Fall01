using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuStatesControler : MonoBehaviour
{
    bool isOnPosition =false;
    public Vector3 desVector;
    RectTransform position;

    private void Start()
    {
        position = GetComponent<RectTransform>();
    }
    private void Update()
    {
        if (!isOnPosition)
        {
            position.anchoredPosition = Vector3.Lerp(position.anchoredPosition, desVector, 5f * Time.deltaTime);

        }

    }

    public void invokerDestroy()
    {
        Invoke("selfDestroy", 0.5f);
    }

    public void selfDestroy()
    {
        Destroy(gameObject);
    }
}
