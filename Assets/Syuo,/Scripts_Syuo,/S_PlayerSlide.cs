
using UnityEngine;
using System.Collections;

public class S_PlayerSlide : MonoBehaviour
{
    [SerializeField] private float slideSpeed;//スライドの速度
    [SerializeField] private float slideTime;//スライドの時間
    [SerializeField] private float attenuationSlide;//スライドの減衰力
    private bool isSlide;//スライド状態かどうか   

    private S_PlayerAnimSystem playerAnimSystem;//S_PlayerAnimSystemコンポーネントへの参照
    private S_PlayerMove playerMove;//S_PlayerMoveコンポーネントへの参照
    private Rigidbody2D rb;//Rigidbody2Dコンポーネントへの参照

    void Start()
    {
        playerAnimSystem = GetComponent<S_PlayerAnimSystem>();//S_PlayerAnimSystemコンポーネントを取得
        playerMove = GetComponent<S_PlayerMove>();//S_PlayerMoveコンポーネントを取得
        rb = GetComponent<Rigidbody2D>();//Rigidbody2Dコンポーネントを取得
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.L) && !isSlide && playerMove.isGrounded && playerMove.moveData.xSpeed != 0)
        {
            isSlide = true;//スライド状態にする
            playerMove.isSlide = true;//スライド状態にする
            rb.linearVelocity = new Vector2(0, 0);//スライド開始時の速度をリセットする
            StartCoroutine(SlideCourtine());//スライドの時間を待つコルーチンを開始する

            playerAnimSystem.SlideAnim();//スライドアニメーションを再生する
        }
    }

    private IEnumerator SlideCourtine()//スライドの時間を待つコルーチン
    {
        rb.linearVelocity = new Vector2(slideSpeed * transform.localScale.x, rb.linearVelocity.y);//スライドの速度を設定する
        yield return new WaitForSeconds(slideTime);//slideTime秒待つ

        float _slideSpeed = slideSpeed;//スライドの速度を格納する変数 
        while (_slideSpeed > 0)
        {
            //Debug.Log("スライドの速度: " + _slideSpeed);//スライドの速度をデバッグログに出力する
            _slideSpeed -= Time.deltaTime * attenuationSlide;//スライドの速度を減少させる
            rb.linearVelocity = new Vector2(_slideSpeed * transform.localScale.x, rb.linearVelocity.y);//スライドの速度を設定する
            yield return null;//次のフレームまで待つ
        }

        isSlide = false;//スライド状態を終了する
        playerMove.isSlide = false;//スライド状態を終了する
        playerAnimSystem.currentAnimState = S_PlayerAnimSystem.PlayerAnimState.Idle;//アニメーション状態をIdleに変更する


    }

}
