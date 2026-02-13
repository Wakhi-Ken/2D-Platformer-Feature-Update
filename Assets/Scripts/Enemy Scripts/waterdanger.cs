using UnityEngine;

public class waterdanger : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            GameManager.instance.LoseLife();
    }

}
