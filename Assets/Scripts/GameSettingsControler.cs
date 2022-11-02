using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettingsControler : MonoBehaviour
{
    public GameObject playerSkin;
    
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }


}
