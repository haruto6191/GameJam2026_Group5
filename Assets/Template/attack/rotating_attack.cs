using UnityEngine;

public class rotating_attack : MonoBehaviour
{
    [Header("‰ñ“]‚µŽn‚ß‚éŽžŠÔ")]
    [SerializeField] float start_time;

    [Header("‰ñ“]‘¬“x")]
    [SerializeField] float speed;

    [Header("‰ñ“]’âŽ~ŽžŠÔ(0‚È‚çŽ~‚Ü‚ç‚È‚¢)")]
    [SerializeField] float stop_time;

    private float count = 0, st_count = 0;

    private bool stop = false;

    void FixedUpdate()
    {
        if (!stop)
        {
            count += Time.deltaTime;

            if (count > start_time)
            {
                Rota();
                if (count > stop_time + start_time)
                {
                    count = start_time;
                    stop = true;
                }
            }
        }
        else
        {
            st_count += Time.deltaTime;

            if (st_count > stop_time)
            {
                st_count = 0;
                stop = false;
            }
        }
    }
    
    void Rota()
    {
        transform.Rotate(0, 0, speed * Time.deltaTime);
    }

    private void OnDisable()
    {
        count = 0;
        st_count = 0;
        stop = false;

        transform.rotation = Quaternion.identity;
    }
}
