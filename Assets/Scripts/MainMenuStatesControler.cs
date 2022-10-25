using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuStatesControler : MonoBehaviour
{
    bool isOnPosition =false;
    public Vector2 desVector;
    private void Update()
    {
        if (!isOnPosition)
        {
            transform.position = Vector2.Lerp(transform.position, desVector, 5f * Time.deltaTime);
            
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
