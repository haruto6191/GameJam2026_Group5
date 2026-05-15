using UnityEngine;

public class DestructibleObject : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
    
        if (other.CompareTag("PlayerAttack"))//このタグはまだ追加してない（仮）
        {
            Destroy(gameObject);
        }
    }
}