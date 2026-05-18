using UnityEngine;
using System.Collections;

//Playerタグを付けてください

public class FallingPlatform : MonoBehaviour
{
    [Header("設定")]
    [SerializeField] private float fallDelay = 1.0f;     // 乗ってから落ちるまでの時間
    [SerializeField] private float destroyDelay = 2.0f;  // 落ちてから消えるまでの時間
    [SerializeField] private float respawnDelay = 3.0f;  // 消えてから復活するまでの時間

    private Rigidbody2D rb;
    private Vector3 initialPosition; // 初期の位置
    private Quaternion initialRotation; // 初期の回転
    private bool isFalling = false;

    private SpriteRenderer spriteRenderer;
    private Collider2D collider2D_w;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // 復活させるために初期位置を記憶
        initialPosition = transform.position;
        initialRotation = transform.rotation;

        spriteRenderer = GetComponent<SpriteRenderer>();
        collider2D_w = GetComponent<Collider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // プレイヤーが上から乗った時だけ処理を開始
        if (collision.gameObject.CompareTag("Player") && !isFalling)
        {
            // 上から乗ったか判定
            foreach (ContactPoint2D contact in collision.contacts)
            {
                //床が落ちるを真上だけにするため
                if (contact.normal.y < -0.5f)
                {
                    StartCoroutine(FallRoutine());//カウント開始
                    break;
                }
            }
        }
    }

    // 以下コンルチーンで落ちて復活するまでの一連の流れ
    private IEnumerator FallRoutine()
    {
        isFalling = true;

        // 1. 少し揺れるなどの予兆を入れる
        float elapsed = 0f;// 経過時間を計る
        Vector3 pos = transform.position;// 揺れ始める前の位置
        while (elapsed < fallDelay)
        {
            transform.position = pos + (Vector3)Random.insideUnitCircle * 0.05f;//Random.insideUnitCircle: 半径1の円の中のどこかランダムな地点を (x, y) で返す
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = pos; // 位置を戻してから落とす
        yield return new WaitForSeconds(fallDelay / 2);

        // 2. 物理演算を有効にして落下させる
        rb.bodyType = RigidbodyType2D.Dynamic;

        // 3. 一定時間後にオブジェクトを非表示にする
        yield return new WaitForSeconds(destroyDelay);
        //gameObject.SetActive(false);
        On_Off(false);

        // 4. 復活処理
        yield return new WaitForSeconds(respawnDelay);
        ResetPlatform();
    }

    //床を復活させない場合は以下の関数を消してください
    private void ResetPlatform()
    {
        //gameObject.SetActive(true);
        On_Off(true);

        isFalling = false;
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        transform.position = initialPosition;
        transform.rotation = initialRotation;
    }

    void On_Off(bool sw)
    {
        spriteRenderer.enabled = sw;
        collider2D_w.enabled = sw;
    }
}