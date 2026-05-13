using UnityEngine;

public class Explosion_Effect : MonoBehaviour
{
    [SerializeField]
    float dis_speed;

    private Color color;
    private SpriteRenderer sprite;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();

        color = sprite.color;
    }

    void FixedUpdate()
    {
        color.a -= Time.deltaTime * dis_speed;
        sprite.color = color; //“§–¾‚É‚·‚é‚¾‚¯‚ÌƒvƒƒOƒ‰ƒ€

        if(color.a <= 0)
        {
            Destroy(gameObject);
        }
    }
}
