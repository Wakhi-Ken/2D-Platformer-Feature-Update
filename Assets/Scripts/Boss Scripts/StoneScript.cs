using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneScript : MonoBehaviour
{

    void Start()
    {
        Invoke("Deactivate", 4f);
    }

    void Deactivate()
    {
        gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == MyTags.PLAYER_TAG)
        {

            // APPLY DAMAGE TO PLAYER (NEW SYSTEM)
            PlayerDamage player = target.GetComponent<PlayerDamage>();
            if (player != null)
            {
                player.HitPlayer(transform.position);
            }

            gameObject.SetActive(false);
        }
    }

} 
// class
