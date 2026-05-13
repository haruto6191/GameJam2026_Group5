using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Boss_move : MonoBehaviour
{
    private int phase = 0;

    private float wait_time = 2;
    [SerializeField] float[] phase_times;

    [SerializeField] GameObject[] phase_attack_obj;

    [SerializeField] Vector2[] move_force,phase1_force, phase2_force;
    [SerializeField] float right_edge, left_edge; //bossステージの右端っこあたり

    //move
    private bool move_now = false;
    private Transform p_transform;
    [SerializeField] float range;//プレイヤーとの距離

    Rigidbody2D rb;
    Collider2D boss_col;

    //とりあえず仮で
    [SerializeField] int Boss_HP = 200;
    private int Boss_maxHP;

    [SerializeField] GameObject Boss_UI;
    [SerializeField] Slider HP_slider;

    private Vector2 initial_pos;

    [SerializeField] Game_Clear_SC Game_Clear;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boss_col = GetComponent<Collider2D>();

        p_transform = GameObject.FindWithTag("Player").transform;

        Collision_Prevention(p_transform.gameObject);

        Boss_maxHP = Boss_HP;

        HP_slider.maxValue = Boss_maxHP;
        HP_slider.value = Boss_HP;

        initial_pos = transform.position;
    }

    private void OnEnable()
    {
        Boss_UI.SetActive(true);

        HP_slider.maxValue = Boss_maxHP;
        HP_slider.value = Boss_HP;
    }

    void FixedUpdate()
    {
        wait_time -= Time.deltaTime; //待機時間を減算

        if(wait_time < 0 && !move_now) //待機時間が0未満になったら入る
        {
            //フェーズ切り替え時にプレイヤーが近かったら移動
            if(Vector3.Distance(transform.position, p_transform.transform.position) < range)
            {
                move_now = true;

                StartCoroutine(BossMove());

                return;
            }
            
            wait_time = phase_times[phase]; //フェーズごとの時間を待機時間へ

            switch (phase) //フェーズごとにswitchで実行
            {
                case 0:
                    phase = 1;
                    StartCoroutine(Omnidirectional_Shooting());
                    break;
                case 1:
                    phase = 2;
                    StartCoroutine(Double_Rocket_Firework());
                    break;
                case 2:
                    phase = 0;
                    StartCoroutine(Give_Firecrackers());
                    break;
            }
        }

        if(Boss_HP <= 0) //ボスのHP(仮)が0以下でゲームクリア
        {
            Game_Clear.GameClear(); //Game_Clearというスクリプト参照
        }
    }
    
    void Collision_Prevention(GameObject obj)
    {
        var col = obj.GetComponent<Collider2D>();

        if(col != null)
        {
            Physics2D.IgnoreCollision(col, boss_col, true); //衝突しないようにする
        }
    }

    IEnumerator BossMove() //boss移動
    {
        if(right_edge < transform.position.x) //bossステージの右端にいるなら
            rb.AddForce(move_force[0], ForceMode2D.Force);
        else if (left_edge > transform.position.x) //bossステージの左端にいるなら
            rb.AddForce(move_force[1], ForceMode2D.Force);
        else if (p_transform.position.x > transform.position.x) //プレイヤーがbossより右にいるなら
            rb.AddForce(move_force[0], ForceMode2D.Force);
        else
            rb.AddForce(move_force[1], ForceMode2D.Force);

        yield return new WaitForSeconds(3f);

        move_now = false;
    }
    
    IEnumerator Omnidirectional_Shooting() //フェーズ0 全方位射撃
    {
        for (int j = 0; j < 3; j++)
        {
            for (int i = 0; i <= 12; i++)
            {
                Instantiate(phase_attack_obj[0], transform.position, Quaternion.Euler(0, 0, -i * 15f));

                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(2f);
        }
    }

    IEnumerator Double_Rocket_Firework() //フェーズ1 左右にロケット花火設置
    {
        var RF1 = Instantiate(phase_attack_obj[1], transform.position, Quaternion.identity);
        var RF1_rb = RF1.GetComponent<Rigidbody2D>();
        RF1_rb.linearVelocity = phase1_force[0];

        Collision_Prevention(RF1);

        yield return new WaitForSeconds(1f);

        var RF2 = Instantiate(phase_attack_obj[1], transform.position, Quaternion.identity);
        var RF2_rb = RF2.GetComponent<Rigidbody2D>();
        RF2_rb.linearVelocity = phase1_force[1];

        Collision_Prevention(RF2);
    }

    IEnumerator Give_Firecrackers() //フェーズ2 かんしゃく玉をプレゼント
    {
        for (int i = 0; i < 16; i++)
        {
            var bomb = Instantiate(phase_attack_obj[2], transform.position, Quaternion.identity);
            var bomb_rb = bomb.GetComponent<Rigidbody2D>();
            bomb_rb.linearVelocity = phase2_force[i];

            Collision_Prevention(bomb);

            yield return new WaitForSeconds(0.15f);
        }
    }

    private void OnDisable()
    {
        Boss_UI.SetActive(false);

        transform.position = initial_pos;
        Boss_HP = Boss_maxHP;

        StopAllCoroutines();

        wait_time = 2;
        phase = 0;
        move_now = false;
    }
}
