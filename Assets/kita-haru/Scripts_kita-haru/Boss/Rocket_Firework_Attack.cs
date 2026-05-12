using System.Collections;
using UnityEngine;

public class Rocket_Firework_Attack : MonoBehaviour
{
    [SerializeField]
    float disappear;

    [SerializeField]
    GameObject Fireworks;


    void Start()
    {
        var p_collider2D = GameObject.FindWithTag("Player").GetComponent<Collider2D>();

        Physics2D.IgnoreCollision(p_collider2D, GetComponent<Collider2D>(), true); //プレイヤーと衝突しないようにする

        Destroy(gameObject, disappear); //n秒後に自動消滅

        StartCoroutine(Start_Shoot());
    }

    void Update()
    {
        
    }

    IEnumerator Start_Shoot() //ロケット花火発射ーー
    {
        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(1.2f);

            Instantiate(Fireworks, transform.position, Quaternion.Euler(0, 0, -i * 10f + 180));
        }
    }
}
