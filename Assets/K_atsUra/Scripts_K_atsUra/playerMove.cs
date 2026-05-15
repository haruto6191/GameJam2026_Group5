using UnityEngine;

public class playerMove : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Rigidbody2D rb;         //物理演算を取得
    private float moveinput;        //動かす方向を保存
    public float movespeed = 8f;    //速度
    public float jumpforce = 12f;   //ジャンプ高さ
    public Transform groundcheck;   //センサー
    public float checkradius = 0.2f;   //センサーの大きさ
    public LayerMask groundlayer;      //レイヤーを判断
    private bool isgrounded;             //地面に接触してるかどうか
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();       //ゲーム開始時に物理演算を取得
    }

    // Update is called once per frame
    void Update()
    {
        moveinput = Input.GetAxisRaw("Horizontal");　　　　//毎フレーム進行方向を取得
        rb.linearVelocity = new Vector2(moveinput * movespeed, rb.linearVelocity.y);  //毎フレーム移動速度を取得
        isgrounded = Physics2D.OverlapCircle(groundcheck.position, checkradius, groundlayer);
        if (Input.GetKeyDown(KeyCode.Space) && isgrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpforce);
        }
    }
}
