using UnityEngine;

public class BouncyFloor : MonoBehaviour
{
    [Header("ギミック設定")]
    [SerializeField] private float bounceForce = 20f; // 跳ね上がる強さ

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 衝突した相手に Rigidbody2D が付いているか確認
        Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            
            foreach (ContactPoint2D contact in collision.contacts)
            {
                // 上から乗ったか判定
                if (contact.normal.y < -0.5f)
                {

                    rb.linearVelocity = new Vector2(rb.linearVelocity.x, bounceForce);

                    //Debug.Log("Player Bounced!");
                    break;
                }
            }
        }
    }
}