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

    [SerializeField]
    private Vector3 Respawn_pos;

    public bool isDead = false;
    private bool isGameOver = false;
    [SerializeField] private GameObject gameOverCanvas;//ゲームオーバー演出用のCanvas
    [SerializeField] private GameObject mainCanvas;//メインのUIを表示するCanvas

    private Transform player;

    public void Awake()
    {
        if (instance == null) instance = this;
    }

    void Start()
    {
        //player_HP = UI_General.instance.manager.player_HP;

        player_maxHP = player_HP;

        HP_slider.maxValue = player_maxHP;
        HP_slider.value = player_HP;

        mainCanvas.SetActive(true);
        gameOverCanvas.SetActive(false);
        isGameOver = false;
        isDead = false;

        player = GameObject.FindGameObjectWithTag("Player").transform;
        Fade_obj.GetComponent<Image>().color = new Color(0, 0, 0, 0);
    }

    [ContextMenu("倒れる")]
    public void Death()
    {
        TakeDamage(10000);
    }


    public void TakeDamage(int damage = 100)
    {
        player_HP -= damage;
        player_HP = Mathf.Clamp(player_HP, 0, player_maxHP);

        HP_slider.value = player_HP;

        if (player_HP <= 0 && !isDead)
        {
            isDead = true;
            StartCoroutine(Death_Direction());
        }
    }

    int PlayerDeath()
    {
        transform.parent.position = Respawn_pos;
        //transform.position = Respawn_pos;

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
        if (collision.gameObject.tag == "DeathArea" && !isDead)
        {
            isDead = true;
            StartCoroutine(Death_Direction());
        }
    }

    IEnumerator Death_Direction() //死亡演出
    {
        //Debug.Log("死亡演出開始前");
        S_PlayerAnimSystem.instance.FallenDownAnim();

        yield return new WaitForSecondsRealtime(2.0f);

        Time.timeScale = 0;

        Debug.Log("死亡演出開始");

        Fade_obj.SetActive(true);

        var fade_img = Fade_obj.GetComponent<Image>();
        Color temp_color = fade_img.color;
        while(fade_img.color.a < 1)
        {
            temp_color.a += Time.unscaledDeltaTime * fade_speed;
            fade_img.color = temp_color;
            yield return new WaitForSecondsRealtime(0.01f);
            Debug.Log("フェードイン中");
        }

        if (PlayerDeath() == 0)
        {
            //ゲームオーバー演出がここぉ
            //Time.timeScale = 0なの注意

            gameOverCanvas.SetActive(true);
            mainCanvas.SetActive(false);

            //Debug.Log("ゲームオーバー");

            isGameOver = true;

            yield break;
        }

        if (!isGameOver)
        {
            S_PlayerAnimSystem.instance.GameStartAnim();
            Time.timeScale = 1;
            player.position = Respawn_pos;
            UI_General.instance.game_time = 300;
            UI_General.instance.exCoin = 0;
            UI_General.instance.GetExCoin(0);
            isDead = false;

        }

        while (fade_img.color.a > 0)
        {
            temp_color.a -= Time.unscaledDeltaTime * fade_speed;
            fade_img.color = temp_color;
            yield return new WaitForSecondsRealtime(0.01f);

            //Debug.Log("フェードアウト中");
        }

        Fade_obj.SetActive(false);
        

    }

    public void Retry()
    {
        Debug.Log("リトライ");
        isDead = false;
        isGameOver = false;

        Fade_obj.GetComponent<Image>().color = new Color(0, 0, 0, 0);

        player.position = Respawn_pos;
        mainCanvas.SetActive(true);
        gameOverCanvas.SetActive(false);

        player_HP = player_maxHP;

        Time.timeScale = 1;

        UI_General.instance.Retry();
        S_PlayerAnimSystem.instance.GameStartAnim();

    }
}
