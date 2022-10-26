using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlayerMainMenu : MonoBehaviour
{
    public float speed;
    public float speedChange;
    Rigidbody2D rigidbody2d;

    float x, y, z;


    private void Start()
    {
        rigidbody2d = gameObject.GetComponent<Rigidbody2D>();

        InvokeRepeating("changeToRandomValue", 0, speedChange);
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Rotate(x * Time.deltaTime, y * Time.deltaTime,0);
    }

    void changeToRandomValue()
    {
        x = Random.Range(-speed, speed);
        y = Random.Range(-speed, speed);
        z = Random.Range(-speed, speed);
    }
}
