using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    public GameObject deathParticles;
    bool isDead;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Instantiate(deathParticles, transform.position, transform.rotation);
        GameObject.Find("EventSystem").GetComponent<PlayerGamePlayControler>().isDead = true;
        Destroy(gameObject);
    }



}
