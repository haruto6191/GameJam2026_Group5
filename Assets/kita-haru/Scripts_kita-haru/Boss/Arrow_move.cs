using UnityEngine;

public class Arrow_move : MonoBehaviour
{
    [SerializeField]
    float disappear, speed;

    void Start()
    {
        Destroy(gameObject, disappear); //nïbå„Ç…é©ìÆè¡ñ≈
    }

    void FixedUpdate()
    {
        transform.Translate(speed, 0, 0);
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("PlayerAttack"))
        {
            Destroy(gameObject);
        }
    }
    
}
