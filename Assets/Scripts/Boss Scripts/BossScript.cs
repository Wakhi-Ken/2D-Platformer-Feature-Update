using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{

    public GameObject stone;
    public Transform attackInstantiate;
    private Animator anim;
    private string coroutine_Name = "StartAttack";

    [Header("Boss Health")]
    public int maxHealth = 5; // Total boss health
    private int currentHealth;

    void Awake()
    {
        anim = GetComponent<Animator>();
        currentHealth = maxHealth;
    }

    void Start()
    {
        StartCoroutine(coroutine_Name);
    }

    void Attack()
    {
        GameObject obj = Instantiate(stone, attackInstantiate.position, Quaternion.identity);
        obj.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-300f, -700), 0f));
    }

    void BackToIdle()
    {
        anim.Play("BossIdle");
    }

    public void DeactivateBossScript()
    {
        StopCoroutine(coroutine_Name);
        enabled = false;
    }

    IEnumerator StartAttack()
    {
        yield return new WaitForSeconds(Random.Range(2f, 5f));
        anim.Play("BossAttack");
        StartCoroutine(coroutine_Name);
    }

    // health
    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        Debug.Log("Boss takes " + damageAmount + " damage. Remaining health: " + currentHealth);

        if (currentHealth <= 0)
        {
            BossDefeated();
        }
    }

    //boss defeat
    public void BossDefeated()
    {
        Debug.Log("Boss defeated!"); // Check if this logs
        DeactivateBossScript(); // Stop attacks
        GameManager.instance.BossDefeated(); // Load WinnerScene
    }
}