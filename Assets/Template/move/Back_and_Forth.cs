using UnityEngine;

public class Back_and_Forth : MonoBehaviour
{
    [Header("進む強さ")]
    [SerializeField] float force;

    [Header("力の加え方を変える <- まぁ、やれば分かるよ")]
    [SerializeField] bool linear_mode;

    [Header("右に切り替えるタイミング")]
    [SerializeField] float max_left;

    [Header("左に切り替えるタイミング")]
    [SerializeField] float max_right;

    [Header("右から力を加えようとするか")]
    [SerializeField] bool start_right;

    private bool switch_now = false;

    private Rigidbody2D rb;

    private Vector2 initial_pos;

    private void Start()
    {
        if(start_right) switch_now = true;

        rb = GetComponent<Rigidbody2D>();

        initial_pos = transform.position;
    }

    void FixedUpdate()
    {
        if (switch_now)
        {
            if (linear_mode) rb.linearVelocity = new Vector2(force, rb.linearVelocityY);
            else rb.AddForce(force * Vector2.right, ForceMode2D.Force);

            if(transform.position.x > initial_pos.x + max_right)
                switch_now = false;
        }
        else
        {
            if (linear_mode) rb.linearVelocity = new Vector2(-force, rb.linearVelocityY);
            else rb.AddForce(-force * Vector2.right, ForceMode2D.Force);

            if (transform.position.x < initial_pos.x - max_left)
                switch_now = true;
        }
    }

    private void OnDisable()
    {
        if (start_right) switch_now = true;
        else switch_now = false;
    }
}
