using UnityEngine;

public class Explosion_Effect : MonoBehaviour
{
    [SerializeField]
    float disappear, dis_speed;

    private Color color;
    private SpriteRenderer sprite;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();

        color = sprite.color;

        Destroy(gameObject, disappear); //n•bŒã‚É©“®Á–Å
    }

    void FixedUpdate()
    {
        color.a -= Time.deltaTime * dis_speed;
        sprite.color = color; //“§–¾‚É‚·‚é‚¾‚¯‚ÌƒvƒƒOƒ‰ƒ€
        /*
        if(color.a < 0.33f)
        {
        //”»’èÁ‚µ‚Ä‚Ë
        }
        */
    }
}
