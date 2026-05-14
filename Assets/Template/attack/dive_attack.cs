using UnityEngine;

public class dive_attack : MonoBehaviour
{
    [Header("許容接近範囲")]
    [SerializeField] float range;

    [Header("速度")]
    [SerializeField] float speed;

    [Header("追尾する時間")]
    [SerializeField] float tracking_time;

    [Header("自爆機能オン　以下自爆機能にのみ関係")]
    [SerializeField] bool self_destruction;

    [Header("爆発するまでの時間")]
    [SerializeField] float explosion_time;

    [Header("爆発の大きさ")]
    [SerializeField] float explosion_size;

    [Header("爆発")]
    [SerializeField] GameObject explosion_effect;


    private Transform p_transform;
    private bool tracking = false, off_tra = false;

    private float count = 0, ex_count = 0;

    private Rigidbody2D rb;

    void Start()
    {
        p_transform = GameObject.FindWithTag("Player").transform;

        //Physics2D.IgnoreCollision(GetComponent<Collider2D>(), p_transform.gameObject.GetComponent<Collider2D>(), true); //衝突しないようにする

        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, p_transform.transform.position) < range && !tracking)
        {
            tracking = true;
            track();
        }

        count += Time.deltaTime;

        if (tracking)
        {
            if (count < tracking_time && !off_tra)
            {
                track();
            }

            Vector2 dir = transform.rotation * Vector2.right;
            rb.AddForce(dir * speed, ForceMode2D.Force);
        }

        if (self_destruction)
        {
            ex_count += Time.deltaTime;

            if(explosion_time < ex_count)
                explosion();
        }
    }

    void track()
    {
        Vector2 dir = p_transform.position - transform.position;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void explosion()
    {
        var b = Instantiate(explosion_effect, transform.position, Quaternion.identity);
        b.transform.localScale = b.transform.localScale * explosion_size;

        gameObject.SetActive(false);
    }

    void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.gameObject.tag == "Player")
        {
            if(self_destruction) explosion();
            else off_tra = true;
        }
    }

    private void OnDisable()
    {
        count = 0;
        tracking_time = 0;
        tracking = false;
        off_tra = false;

        ex_count = 0;
        transform.localRotation = Quaternion.identity;
    }
}
