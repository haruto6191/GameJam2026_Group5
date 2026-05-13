using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Exp_player_status : MonoBehaviour
{
    public static Exp_player_status instance; //グローバル化

    public int player_HP = 100;
    private int player_maxHP;

    [SerializeField]
    private Slider HP_slider;


    [SerializeField]
    private GameObject Fade_obj;
    [SerializeField]
    private float fade_speed;

    public void Awake()
    {
        if (instance == null) instance = this;
    }

    void Start()
    {
        player_maxHP = player_HP;

        HP_slider.maxValue = player_maxHP;
        HP_slider.value = player_HP;
    }

    void FixedUpdate()
    {
        
    }

    public void TakeDamage(int damage)
    {
        player_HP -= damage;
        player_HP = Mathf.Clamp(player_HP, 0, player_maxHP);

        HP_slider.value = player_HP;

        if (player_HP <= 0)
        {
            StartCoroutine(Death_Direction());
        }
    }

    int PlayerDeath()
    {
        transform.position = new Vector3(0, 0, 0);

        //HierarchyにUI_Generalがなかったら使わんでね
        UI_General.instance.GetLife(-1);

        player_HP = player_maxHP;
        HP_slider.value = player_HP;

        //ステージ上の敵をリセット
        GameObject[] ene = GameObject.FindGameObjectsWithTag("Enemy");
        for(int i = 0; i < ene.Length; i++)
            ene[i].SetActive(false);

        return (UI_General.instance.life <= 0) ? 0 : 1;
    }

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.tag == "EnemyAttack")
        {
            TakeDamage(20);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "DeathArea")
        {
            StartCoroutine(Death_Direction());
        }
    }

    IEnumerator Death_Direction() //死亡演出
    {
        Time.timeScale = 0;

        Fade_obj.SetActive(true);

        var fade_img = Fade_obj.GetComponent<Image>();
        Color temp_color = fade_img.color;
        while(fade_img.color.a < 1)
        {
            temp_color.a += Time.unscaledDeltaTime * fade_speed;
            fade_img.color = temp_color;
            yield return new WaitForSecondsRealtime(0.01f);
        }

        if (PlayerDeath() == 0)
        {
            //ゲームオーバー演出がここぉ
            //Time.timeScale = 0なの注意
            yield break;
        }

        while (fade_img.color.a > 0)
        {
            temp_color.a -= Time.unscaledDeltaTime * fade_speed * 2;
            fade_img.color = temp_color;
            yield return new WaitForSecondsRealtime(0.01f);
        }

        Fade_obj.SetActive(false);

        Time.timeScale = 1;
    }
}
