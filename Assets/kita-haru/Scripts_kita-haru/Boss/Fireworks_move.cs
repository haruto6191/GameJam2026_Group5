using UnityEngine;

public class Fireworks_move : MonoBehaviour
{
    public float start_tra,tracking_time, disappear, speed, size = 1;

    [SerializeField]
    GameObject bomb;

    private Transform player;

    private float count = 0;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform; //playerのtransformを取得

        Destroy(gameObject, disappear); //n秒後に自動消滅
    }

    void FixedUpdate()
    {
        count += Time.deltaTime; //生成されてからの時間を計測

        if (start_tra < 0 && count < tracking_time) //以下プレイヤーに向かって向きを変えるやーつ
        {
            Vector2 dir = player.position - transform.position;

            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
        else
        {
            start_tra -= Time.deltaTime;
        }

        transform.Translate(speed, 0, 0); //進んでください
    }

    //プレイヤーもしくは地面にあたれば破壊
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (/*collision.gameObject.CompareTag("Ground") || */collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
    
    //破壊時に爆破エフェクトを生成
    void OnDestroy()
    {
        var b = Instantiate(bomb, transform.position, Quaternion.identity);
        b.transform.localScale = b.transform.localScale * size;
    }
}
