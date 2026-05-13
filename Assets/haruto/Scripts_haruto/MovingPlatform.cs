using UnityEngine;

//Uniy上でCreateEmptyとしてPointA, PointBを作成し、その2点間を床が往復し続ける。
public class MovingPlatform : MonoBehaviour
{
    [Header("移動設定")]
    [SerializeField] private Transform pointA; // 地点A
    [SerializeField] private Transform pointB; // 地点B
    [SerializeField] private float speed = 3f; // 移動速度

    private Vector3 target; // 現在の目的地

    void Start()
    {
        // 最初は地点Aを目指す
        target = pointA.position;
    }

    void Update()
    {
        // 現在地から目的地へ移動
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        // 目的地に到着したら入れ替える
        if (Vector3.Distance(transform.position, target) < 0.1f)
        {
            target = target == pointA.position ? pointB.position : pointA.position;
        }
    }

    //プレイヤーを床の子要素にする処理
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // プレイヤーが上に乗ったら、プレイヤーの親をこの床にする
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // プレイヤーが離れたら、親を解除する
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}