using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    public GameObject deathParticles;

    public void killPlayer()
    {
        Instantiate(deathParticles, transform.position, transform.rotation);
        GameObject.Find("EventSystem").GetComponent<GameplayControler>().isDead = true;
        Destroy(gameObject);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        killPlayer();
    }



}
