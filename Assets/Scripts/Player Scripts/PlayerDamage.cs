using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerDamage : MonoBehaviour
{

    private Text lifeText;
    private int lifeScoreCount;

    private bool canDamage;
    private Vector3 respawnPosition;

    void Awake()
    {
        lifeText = GameObject.Find("LifeText").GetComponent<Text>();
        lifeScoreCount = 3;
        lifeText.text = "x" + lifeScoreCount;

        canDamage = true;
        respawnPosition = transform.position;
    }

    void Start()
    {
        Time.timeScale = 1f;
    }

    void Update()
    {
        // PLAYER FALLS OFF MAP → RESTART GAME
        if (transform.position.y < -10f)
        {
            SceneManager.LoadScene("GameScene-ALU");
        }
    }

    public void DealDamage()
    {
        if (canDamage)
        {

            lifeScoreCount--;

            if (lifeScoreCount >= 0)
            {
                lifeText.text = "x" + lifeScoreCount;
            }

            if (lifeScoreCount == 0)
            {
                // RESTART THE GAME
                Time.timeScale = 0f;
                StartCoroutine(RestartGame());
            }

            canDamage = false;
            StartCoroutine(WaitForDamage());
        }
    }

    IEnumerator WaitForDamage()
    {
        yield return new WaitForSeconds(2f);
        canDamage = true;
    }

    IEnumerator RestartGame()
    {
        yield return new WaitForSecondsRealtime(2f);
        SceneManager.LoadScene("Gameplay");
    }

    // ENEMIES CALL THIS FUNCTION
    public void HitPlayer(Vector2 enemyPosition)
    {
        if (!canDamage) return;

        DealDamage();

        // PUSH PLAYER AWAY ONLY IF TESTER EXISTS (prevents crash)
        TESTER pushScript = GetComponent<TESTER>();
        if (pushScript != null)
        {
            pushScript.DamageAndPush(transform.position.x < enemyPosition.x ? -300f : 300f);
        }

        RespawnPlayer();
    }

    void RespawnPlayer()
    {
        transform.position = respawnPosition;
        GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
    }

} // class
