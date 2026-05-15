using UnityEngine;

public class S_BouncyFloor : MonoBehaviour
{
    [Header("ギミック設定")]
    [SerializeField] private float bounceForce = 20f; // 跳ね上がる強さ
    [SerializeField] private float bounceRange = 0.5f;

    private S_PlayerMove playerMove;

    void Start()
    {
        playerMove = S_PlayerMove.instance;
        if(playerMove == null)
        {
            Debug.LogError("S_PlayerMove instance not found in the scene.");
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 衝突した相手に Rigidbody2D が付いているか確認
        Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
        


        if (rb != null)
        {
            
            foreach (ContactPoint2D contact in collision.contacts)
            {
                // 上から乗ったか判定
                if (contact.normal.y < bounceRange)
                {

                    playerMove.moveData.ySpeed += bounceForce;

                    Debug.Log("Player Bounced!");
                    break;
                }
            }
        }
    }
}