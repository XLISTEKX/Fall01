using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertControler : MonoBehaviour
{
    public GameObject deathRegion;

    public float speed;
    public float changeTime;

    public float speedRate = 1;

    bool isActive;
    BoxCollider2D boxCollider2D;
    private void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        InvokeRepeating("changeRegionState", changeTime, changeTime);
        speedRate = 1 + GameObject.Find("EventSystem").GetComponent<GameplayControler>().score * 0.1f;
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, speed * speedRate);

    }
    void changeRegionState()
    {
        if (isActive)
        {
            isActive = false;
            deathRegion.SetActive(false);
            boxCollider2D.enabled = false;
            return;
        }
        isActive = true;
        deathRegion.SetActive(true);
        boxCollider2D.enabled = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.GetComponent<PlayerControler>().killPlayer();
        }
    }
}
