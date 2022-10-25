
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BIControler : MonoBehaviour
{
    public Sprite secondSprite;

    public float changeTimeRange;
    public float speed;


    private void Start()
    {
        float x = Random.Range(0.5f, changeTimeRange);

        Invoke("changeSprite", x);

        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, speed);

    }

    void changeSprite()
    {
        Sprite second = gameObject.GetComponent<SpriteRenderer>().sprite;

        gameObject.GetComponent<SpriteRenderer>().sprite = secondSprite;

        secondSprite = second;

        float x = Random.Range(0.5f, changeTimeRange);

        Invoke("changeSprite", x);
    }
}
