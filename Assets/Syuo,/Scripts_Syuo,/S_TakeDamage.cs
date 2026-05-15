using UnityEngine;

public class S_TakeDamage : MonoBehaviour
{
    private S_PlayerAnimSystem playerAnimSystem;

    void Start()
    {
        playerAnimSystem = transform.parent.GetComponent<S_PlayerAnimSystem>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyAttack"))
        {
            playerAnimSystem.TakeDamageAnim();
        }
    }
}

