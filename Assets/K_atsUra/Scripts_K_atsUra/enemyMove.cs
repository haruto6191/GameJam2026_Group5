using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    private Rigidbody2D rb;
    public float movespeed = 3f;   // 敵は少し遅めがおすすめ
    private float direction = 1f;  // 1なら右、-1なら左

    public Transform wallCheck;    // 壁や崖を検知するポイント
    public float checkRadius = 0.2f;
    public LayerMask groundLayer;  // 地面や壁のレイヤー

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rb.linearVelocity = new Vector2(direction * movespeed, rb.linearVelocity.y);

        bool isBlocked = Physics2D.OverlapCircle(wallCheck.position, checkRadius, groundLayer);

        // directionと進んでいる方向が一致している時だけ反転を許可する
        if (isBlocked)
        {
            // 少しだけ壁から離れる方向に進ませるか、強制的に向きを確定させる
            if (direction > 0)
            {
                direction = -1f;
            }
            else
            {
                direction = 1f;
            }

            // 向きに合わせて見た目を変える
            transform.localScale = new Vector3(direction, 1, 1);

            // 【重要】重なりによる連続反転を防ぐため、少しだけ位置をずらす（微調整）
            transform.position += new Vector3(direction * 0.1f, 0, 0);
        }
    }

    void Flip()
    {
        // 方向を反転
        direction *= -1f;
        // 見た目も反転させる
        transform.localScale = new Vector3(direction, 1, 1);
    }
}