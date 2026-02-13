using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    private bool canDamage = true;
    private Vector3 respawnPosition;

    void Awake()
    {
        respawnPosition = transform.position;
    }

    public void HitPlayer(Vector2 enemyPosition)
    {
        if (!canDamage) return;

        canDamage = false;
        Invoke(nameof(ResetDamage), 2f);

        // Tell GameManager we lost a life ❤️
        GameManager.instance.LoseLife();

        // Push player away if script exists
        TESTER pushScript = GetComponent<TESTER>();
        if (pushScript != null)
        {
            pushScript.DamageAndPush(transform.position.x < enemyPosition.x ? -300f : 300f);
        }
    }

    void ResetDamage()
    {
        canDamage = true;
    }
}
