using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FPSCounterControler : MonoBehaviour
{
    public TMP_Text text;
    public int avgFrameRate;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float current = 0;
        current = Time.frameCount / Time.time;
        avgFrameRate = (int)current;
        text.text = avgFrameRate.ToString() + " FPS";
    }
}
