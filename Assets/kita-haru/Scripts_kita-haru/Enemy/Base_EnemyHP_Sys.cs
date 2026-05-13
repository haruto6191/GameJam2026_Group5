using UnityEngine;
using UnityEngine.UI;

public class Base_EnemyHP_Sys : MonoBehaviour
{
    [SerializeField] int Enemy_HP = 200;
    private int Enemy_maxHP;

    [SerializeField] Canvas EnemyHPGauge;
    [SerializeField] Slider Common_slider;
    private Slider HP_slider;
    private RectTransform HP_rectTransform;

    [SerializeField] Vector3 Offset;

    private Vector2 initial_pos;

    void Start()
    {
        initial_pos = transform.position;
    }

    private void OnEnable()
    {
        if (HP_slider == null)
        {
            HP_slider = Instantiate(Common_slider, transform.position, Quaternion.identity, EnemyHPGauge.transform);

            HP_rectTransform = HP_slider.GetComponent<RectTransform>();

            Enemy_maxHP = Enemy_HP;
        }
        else
            HP_slider.gameObject.SetActive(true);

        HP_slider.maxValue = Enemy_maxHP;
        HP_slider.value = Enemy_HP;
    }

    void FixedUpdate()
    {
        if (HP_rectTransform != null)
            HP_rectTransform.position = transform.position + Offset;
    }

    public void TakeDamage(int damage)
    {
        Enemy_HP -= damage;
        Enemy_HP = Mathf.Clamp(Enemy_HP, 0, Enemy_maxHP);

        HP_slider.value = Enemy_HP;

        if (Enemy_HP <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.tag == "EnemyAttack") // <- PlayerAttackに変更な
        {
            TakeDamage(20);
        }
    }

    private void OnDisable()
    {
        if(HP_slider != null)
            HP_slider.gameObject.SetActive(false);

        transform.position = initial_pos;
        Enemy_HP = Enemy_maxHP;

        //敵のプログラムの方でもOnDisable()で行動をリセット
    }

}
