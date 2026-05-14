using UnityEngine;

public class Jumping : MonoBehaviour
{
    [Header("ƒWƒƒƒ“ƒv‚Ì‹­‚³")]
    [SerializeField] float force;

    [Header("ŠÔŠu(’Z‚¢‚Æ‹ó‚Ì”Ş•û‚És‚«‚Ü‚·)")]
    [SerializeField] float loop_time;

    private float count = 0;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        count += Time.deltaTime;

        if (count > loop_time)
        {
            count = 0;

            rb.AddForce(force * 100 * Vector2.up, ForceMode2D.Force);
        }
    }

    private void OnDisable()
    {
        count = 0;
    }
}
