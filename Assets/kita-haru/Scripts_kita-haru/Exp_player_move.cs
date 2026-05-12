using UnityEngine;

public class Exp_player_move : MonoBehaviour
{
    private float movement_X;

    [SerializeField] float move_speed, jump_force;

    private bool ground = false;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //このスクリプトが付いているゲームオブジェクトからRigidbody2Dを取得
    }

    private void Update()
    {
        if (Time.timeScale == 1) //Time.timeScaleが1の間だけ通す、Time.timeScaleについては自分で調べてな
        {
            movement_X = Input.GetAxisRaw("Horizontal"); //キーボードa,d(LeftArrow,RightArrow)の入力を取得

            if (Input.GetKeyDown(KeyCode.Space) && ground) //ジャンプのプログラムだけど、tagが設定できてないので動きません
            {
                rb.linearVelocity = new Vector2(rb.linearVelocityX, jump_force);
            }
        }
    }
    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(movement_X * move_speed, rb.linearVelocityY); //22で取得した結果を反映、aを入力していたら左に動く
    }

    //以下、地面に触れているか等のプログラム、まだ使えないので気にせず
/*
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            ground = true;
        }

        if (collision.gameObject.tag == "Death_Zone")
        {
            Death();
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            ground = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            ground = false;
        }
    }
*/
}
