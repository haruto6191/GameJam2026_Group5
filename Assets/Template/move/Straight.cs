using UnityEngine;

public class Straight : MonoBehaviour
{
    [Header("i‚Ş‹­‚³")]
    [SerializeField] float force;

    [Header("—Í‚Ì‰Á‚¦•û‚ğ•Ï‚¦‚é <- ‚Ü‚ŸA‚â‚ê‚Î•ª‚©‚é‚æ")]
    [SerializeField] bool linear_mode;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if(linear_mode) rb.linearVelocity = new Vector2(force, rb.linearVelocityY);
        else rb.AddForce(force * Vector2.right, ForceMode2D.Force);
    }
}
