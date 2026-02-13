using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{

    private Animator anim;
    private int health = 10;
    private bool canDamage;

    void Awake()
    {
        anim = GetComponent<Animator>();
        canDamage = true;
    }

    IEnumerator WaitForDamage()
    {
        yield return new WaitForSeconds(2f);
        canDamage = true;
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if (canDamage)
        {
            if (target.tag == MyTags.BULLET_TAG)
            {
                health--;
                canDamage = false;

                if (health <= 0)
                {
                    // Reference the BossScript and call BossDefeated
                    BossScript bossScript = GetComponent<BossScript>();
                    if (bossScript != null)
                    {
                        bossScript.BossDefeated(); // Trigger boss defeat
                    }
                    anim.Play("BossDead");
                }

                StartCoroutine(WaitForDamage());
            }
        }
    }
}