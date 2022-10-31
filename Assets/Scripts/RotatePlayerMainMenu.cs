using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlayerMainMenu : MonoBehaviour
{
    public float speed;
    public float speedChange;
    Rigidbody2D rigidbody2d;

    float x, y;


    private void Start()
    {
        rigidbody2d = gameObject.GetComponent<Rigidbody2D>();

        InvokeRepeating("changeToRandomValue", 0, speedChange);
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Rotate(x * Time.deltaTime * speed, y * Time.deltaTime * speed,0);
    }

    void changeToRandomValue()
    {
        x = Random.Range(-1f, 1f);
        y = Random.Range(-1f, 1f);

    }
}
