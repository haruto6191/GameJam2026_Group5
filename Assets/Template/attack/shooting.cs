using System.Collections;
using UnityEngine;

public enum ArrowPattern
{
    Random_Simultaneous,
    Random_Rapid_Fire,
    Player_Aimed_Rapid_Fire,
    Player_Aimed_Simultaneous,
    Chain_Fire
}

public class shooting : MonoBehaviour
{
    [Header("ä‘äu")]
    [SerializeField] float loop_time;
    private float count = 0;

    [Header("ñÓÇÃêî")]
    [SerializeField] int amount;

    [Header("ñÓÇë≈Ç¬ä‘äu <- SimultaneousÇÕä÷åWÇ»Ç¢")]
    [SerializeField] float interval;

    [Header("ñÓÇÃïù <- Rapid_FireÇÕä÷åWÇ»Ç¢")]
    [SerializeField] float angle;

    [Header("äJénÇÃäpìx <- Chain_FireÇÃÇ›ä÷åW")]
    [SerializeField] float start_angle;

    [Header("îΩéûåvâÒÇË <- Chain_FireÇÃÇ›ä÷åW")]
    [SerializeField] bool counterclockwise;
    private int int_counterclockwise;

    [Header("ÉpÉ^Å[Éì <- Ç¢ÇÎÇ¢ÇÎééÇµÇƒÇ›Çƒ")]
    [SerializeField] ArrowPattern pattern;

    [Header("ñÓ")]
    [SerializeField] GameObject arrow;

    private Transform p_transform;

    private void Start()
    {
        p_transform = GameObject.FindWithTag("Player").transform;

        int_counterclockwise = counterclockwise ? -1 : 1;
    }

    void FixedUpdate()
    {
        count += Time.deltaTime;

        if (count > loop_time)
        {
            count = 0;

            switch (pattern)
            {
                case ArrowPattern.Random_Rapid_Fire:
                    StartCoroutine(Rapid_Fire(Random.Range(-180, 181)));
                    break;
                case ArrowPattern.Random_Simultaneous:
                    StartCoroutine(Simu(Random.Range(-180, 181)));
                    break;
                case ArrowPattern.Player_Aimed_Rapid_Fire:
                    Vector2 dir = p_transform.position - transform.position;
                    float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                    StartCoroutine(Rapid_Fire(angle + 180));
                    break;
                case ArrowPattern.Player_Aimed_Simultaneous:
                    Vector2 dir1 = p_transform.position - transform.position;
                    float angle1 = Mathf.Atan2(dir1.y, dir1.x) * Mathf.Rad2Deg;
                    StartCoroutine(Simu(angle1 + 180));
                    break;
                case ArrowPattern.Chain_Fire:
                    StartCoroutine(Chain());
                    break;
            }
        }
    }

    IEnumerator Rapid_Fire(float r)
    {
        for (int i = 0; i < amount; i++)
        {
            Instantiate(arrow, transform.position, Quaternion.Euler(0, 0, r));

            yield return new WaitForSeconds(interval);
        }
    }

    IEnumerator Simu(float r)
    {
        int k = (amount % 2 == 0) ? 0 : 1;
        for (int i = -amount / 2; i < amount / 2 + k; i++)
        {
            Instantiate(arrow, transform.position, Quaternion.Euler(0, 0, r + i * angle));
        }
        yield return new WaitForSeconds(0);
    }

    IEnumerator Chain()
    {
        for (int i = 0; i <= amount; i++)
        {
            Instantiate(arrow, transform.position, Quaternion.Euler(0, 0, -i * angle * int_counterclockwise + start_angle));

            yield return new WaitForSeconds(interval);
        }
    }

    private void OnDisable()
    {
        count = 0;
    }
}
