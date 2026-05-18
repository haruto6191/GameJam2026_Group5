using UnityEngine;

public class surprise_attack : MonoBehaviour
{
    [Header("çUåÇäJénîÕàÕ")]
    [SerializeField] float start_range;

    [Header("ë¨ìx")]
    [SerializeField] float speed;

    private Transform p_transform;
    private bool attack = false;

    private SpriteRenderer spriteRenderer;
    private Color color;

    private bool fiarst = false;

    private Base_EnemyHP_Sys sys;

    private Rigidbody2D rb;

    void Start()
    {
        p_transform = GameObject.FindWithTag("Player").transform;

        spriteRenderer = GetComponent<SpriteRenderer>();

        color = spriteRenderer.color;
        color.a = 0;
        spriteRenderer.color = color;

        //Physics2D.IgnoreCollision(GetComponent<Collider2D>(), p_transform.gameObject.GetComponent<Collider2D>(), true); //è’ìÀÇµÇ»Ç¢ÇÊÇ§Ç…Ç∑ÇÈ

        sys = GetComponent<Base_EnemyHP_Sys>();
        if(sys != null)
        {
            sys.enabled = false;
            sys.HP_slider.gameObject.SetActive(false);
        }

        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (p_transform.transform.position.x - transform.position.x > start_range && !fiarst)
        {
            track();
            attack = true;

            fiarst = true;

            color.a = 1;
            spriteRenderer.color = color;

            if (sys != null)
            {
                sys.HP_slider.gameObject.SetActive(true);
                sys.enabled = true;
            }
        }

        if (attack)
        {
            Vector2 dir = transform.rotation * Vector2.right;
            rb.AddForce(dir * speed, ForceMode2D.Force);
        }
    }

    void track()
    {
        Vector2 dir = p_transform.position - transform.position;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void OnDisable()
    {
        attack = false;
        transform.localRotation = Quaternion.identity;

        fiarst = false;

        color.a = 0;
        spriteRenderer.color = color;

        if (sys != null)
        {
            sys.enabled = false;
            if(sys.HP_slider != null) sys.HP_slider.gameObject.SetActive(false);
        }
    }
}
