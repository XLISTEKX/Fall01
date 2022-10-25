using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    public GameObject deathParticles;



    private void OnCollisionEnter2D(Collision2D collision)
    {
        Instantiate(deathParticles, transform.position, transform.rotation);
        GameObject.Find("EventSystem").GetComponent<GameplayControler>().isDead = true;
        Destroy(gameObject);
    }



}
