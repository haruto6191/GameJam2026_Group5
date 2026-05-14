using System.Collections;
using UnityEngine;

public class bomber_attack : MonoBehaviour
{
    [Header("ŠÔŠu")]
    [SerializeField] float loop_time;
    private float count = 0;

    [Header("amount * 2 = “Š‚°‚é”š’e‚Ì”")]
    [SerializeField] int amount;

    [Header("“Š‚°‚é‹­‚³")]
    [SerializeField] float force;

    [Header("“Š‚°‚éŒü‚«(-‚È‚ç‰ºŒü‚«‚É)")]
    [SerializeField] float vertical;

    [Header("”š’e‚Ì‘å‚«‚³")]
    [SerializeField] float bomb_size;

    [Header("”š”­‚·‚é‚Ü‚Å‚ÌŠÔ")]
    [SerializeField] float explosion_time;

    [Header("”š”­‚Ì‘å‚«‚³")]
    [SerializeField] float explosion_size;

    [Header("”š’e")]
    [SerializeField] GameObject bomb;

    private Collider2D my_col;

    private void Start()
    {
        my_col = GetComponent<Collider2D>();
    }

    void FixedUpdate()
    {
        count += Time.deltaTime;

        if(count > loop_time)
        {
            count = 0;
            StartCoroutine(Bomber());
        }
    }

    IEnumerator Bomber()
    {
        for (int i = 0; i < amount * 2; i++)
        {
            var b1 = Instantiate(bomb, transform.position, Quaternion.identity);
            var b1_rb = b1.GetComponent<Rigidbody2D>();

            if(i % 2 == 0)
                b1_rb.linearVelocity = new Vector2((i / 2 + 1) * force, vertical);
            else
                b1_rb.linearVelocity = new Vector2((i / 2 + 1) * -force, vertical);

            b1.transform.localScale = b1.transform.localScale * bomb_size;

            var b1_comp = b1.GetComponent<Fireworks_move>();
            if(b1_comp != null)
            {
                b1_comp.disappear = explosion_time;
                b1_comp.size = explosion_size;
            }

            Collision_Prevention(b1);

            yield return new WaitForSeconds(0.15f);
        }
    }

    void Collision_Prevention(GameObject obj)
    {
        var col = obj.GetComponent<Collider2D>();

        if (col != null)
        {
            Physics2D.IgnoreCollision(col, my_col, true); //Õ“Ë‚µ‚È‚¢‚æ‚¤‚É‚·‚é
        }
    }

    private void OnDisable()
    {
        count = 0;
    }
}
