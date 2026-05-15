using UnityEngine;
using System.Collections;
using Spine.Unity;

public class S_PlayerMove : MonoBehaviour
{
    private Rigidbody2D rb;

    [System.Serializable]
    public class MoveData//移動に関するデータ
    {
        public float mainSpeedX;//基本速度
        public float mainSpeedY;//基本速度
        public float xSpeed;//横移動の速度
        public float ySpeed;//縦移動の速度
        public float jumpTime;//ジャンプの時間
        public float attenuationJump;//ジャンプの減衰力
        public bool isLeft;//左向きかどうか
    }

    public MoveData moveData;//移動に関するデータを格納する変数
    private bool updateAnimation;//アニメーションを更新するかどうか
    private bool isIdle;//アイドル状態かどうか
    private float size;//プレイヤーのサイズを格納する変数

    [SerializeField] private Transform groundCheck; // 足元に配置する空のオブジェクト
    [SerializeField] private float groundCheckRadius = 0.2f; // 判定の半径
    [SerializeField] private LayerMask floorLayer; // Inspectorで「Floor」レイヤーを指定
    private float jumpTimeCounter;//ジャンプの時間をカウントする変数
    public bool isJump;//ジャンプ状態（上昇終了または落下中）かどうか
    public bool isGrounded;//地面に接地しているかどうか
    private bool canDoubleJump;//二段ジャンプ（空中ジャンプ）ができるかどうか

    public bool isSlide;//スライド状態かどうか

    [SerializeField] private GameObject dashEffectR;//ダッシュエフェクト
    [SerializeField] private GameObject dashEffectL;//ダッシュエフェクト

    private S_PlayerAnimSystem playerAnimSystem;//S_PlayerAnimSystemコンポーネントへの参照

    [SerializeField] private GameObject mainCamera;//むりやりカメラ追従
    [SerializeField] private bool isSecondJumpEnabled = true;//二段ジャンプが有効かどうか
    //[SerializeField] private Transform reaf;//リーフのTransformへの参照


    private bool isSquat;//しゃがみ状態かどうか
    /*
    [SerializeField] private GameObject bg;
    private float bgXPos;
    private float bgYPos;
    */

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();//プレイヤーからRigidbody2Dを取得
        if (rb == null)
        {
            Debug.LogError("Rigidbody2Dが見つかりませんでした。");
        }

        playerAnimSystem = GetComponent<S_PlayerAnimSystem>();//S_PlayerAnimSystemコンポーネントを取得

        isJump = false;//ジャンプしていない状態にする
        jumpTimeCounter = 0;//ジャンプの時間を0にする
        updateAnimation = true;//アニメーションを更新する状態にする

        size = transform.localScale.x;//プレイヤーのサイズを取得する
        isIdle = true;//アイドル状態にする

        dashEffectR.SetActive(false);//ダッシュエフェクトを非表示にする
        dashEffectL.SetActive(false);//ダッシュエフェクトを非表示にする

           // bgXPos = bg.transform.localPosition.x;//背景のX座標を取得する
           // bgYPos = bg.transform.localPosition.y;//背景のY座標を取得する
    }

    private void Update()
    {
        if(isSlide)
            return;//スライド状態のときは移動処理を行わない

        //----------------------------------------------------[横移動処理]----------------------------------------------------------

        if (Input.GetKey(KeyCode.A))//Aキーを押したとき
        {
            if (moveData.isLeft)//右向きのとき
            {
                updateAnimation = true;//アニメーションを更新する状態にする
            }
            moveData.isLeft = true;//左向きにする
            moveData.xSpeed = -moveData.mainSpeedX;//横移動の速度を負にする
            playerAnimSystem.DashAnim();//ダッシュアニメーションを再生する  

            
            
            
            if (!dashEffectR.activeSelf)//右向きのとき
            {
                dashEffectR.SetActive(true);//ダッシュエフェクトを表示する
                dashEffectL.SetActive(false);//ダッシュエフェクトを非表示にする
            }
        }
        else if (Input.GetKey(KeyCode.D))//Dキーを押したとき
        {
            if (!moveData.isLeft)//左向きのとき
            {
                updateAnimation = true;//アニメーションを更新する状態にする
            }
            moveData.isLeft = false;//右向きにする
            moveData.xSpeed = moveData.mainSpeedX;//横移動の速度を正にする
            playerAnimSystem.DashAnim();//ダッシュアニメーションを再生する

            if (!dashEffectL.activeSelf)//右向きのとき
            {
                dashEffectR.SetActive(false);//ダッシュエフェクトを非表示にする
                dashEffectL.SetActive(true);//ダッシュエフェクトを表示する
            }
        }
        else
        {
            if(!isIdle)//アイドル状態でないとき
            {
                playerAnimSystem.IdleAnim();//アイドルアニメーションを再生する
                dashEffectL.SetActive(false);//ダッシュエフェクトを非表示にする
                dashEffectR.SetActive(false);//ダッシュエフェクトを非表示にする
            }
            moveData.xSpeed = 0;//横移動の速度を0にする
            isIdle = true;//アイドル状態にする


            if(Input.GetKey(KeyCode.S))//Sキーを押したとき
            {
                if(!isSquat)//しゃがみ状態でないとき
                {
                    isSquat = true;//しゃがみ状態にする
                    playerAnimSystem.SquatAnim();//しゃがみアニメーションを再生する
                }
           
            }
            else if (isSquat)
            {
                isSquat = false;
                playerAnimSystem.EmptyAnim();//アニメーションをリセットする
                playerAnimSystem.currentAnimState = S_PlayerAnimSystem.PlayerAnimState.Idle;//アニメーション状態をIdleにする
            }

        }

        if (moveData.isLeft && updateAnimation)//左向きのとき
        {
            transform.localScale = new Vector3(-size, size, 1);//プレイヤーを反転させる
            mainCamera.transform.localPosition = new Vector3(-30, 27, -10);//カメラを反転させる
            //bg.transform.localPosition = new Vector3(-bgXPos, bgYPos, 0);//背景を反転させる
            //reaf.localScale = new Vector3(-1, 1, 1);//リーフを反転させる
        }
        else if (!moveData.isLeft && updateAnimation)//右向きのとき
        {
            transform.localScale = new Vector3(size, size, 1);//プレイヤーを元に戻す
            mainCamera.transform.localPosition = new Vector3(30, 27, -10);//カメラを反転させる
            //bg.transform.localPosition = new Vector3(bgXPos, bgYPos, 0);//背景を反転させる
            //reaf.localScale = new Vector3(1, 1, 1);//リーフを元に戻す
        }

        if(isIdle && moveData.xSpeed != 0)
        {
            isIdle = false;//アイドル状態を解除する
        }

        //----------------------------------------------------[横移動処理]----------------------------------------------------------

        //----------------------------------------------------[縦移動処理]----------------------------------------------------------

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, floorLayer);//地面に接地しているかどうかを判定する

        // 接地しているときの初期化処理
        if (isGrounded)
        {
            moveData.ySpeed = 0; // 縦速度をリセットする
            jumpTimeCounter = 0;
            isJump = false;
            canDoubleJump = true; // 空中ジャンプ権を回復する

            if (playerAnimSystem.currentAnimState == S_PlayerAnimSystem.PlayerAnimState.Jump)//ジャンプアニメーションの状態であれば
            {
                playerAnimSystem.currentAnimState = S_PlayerAnimSystem.PlayerAnimState.Idle; // アニメーション状態をIdleにする
                playerAnimSystem.EmptyAnim(); // アニメーションをリセットする
            }
                
        }
        else
        {
            if(dashEffectL.activeSelf || dashEffectR.activeSelf)//空中にいるとき
            {
                dashEffectL.SetActive(false);//ダッシュエフェクトを非表示にする
                dashEffectR.SetActive(false);//ダッシュエフェクトを非表示にする
            }

        }

        // 空中での二段ジャンプ（または落下時の空中ジャンプ）開始処理
        if (Input.GetKeyDown(KeyCode.Space) && !isGrounded && canDoubleJump && isSecondJumpEnabled)
        {

            canDoubleJump = false; // 空中ジャンプ権を消費
            jumpTimeCounter = 0;   // ジャンプ時間をリセットし、再度長押しジャンプを可能にする
            isJump = false;        // ジャンプ上昇可能状態にする
            moveData.ySpeed = moveData.mainSpeedY; // 現在の落下速度をキャンセルし、上向きの速度を設定
        }

        // ジャンプ上昇処理（1段目・2段目共通）
        if (Input.GetKey(KeyCode.Space) && (jumpTimeCounter < moveData.jumpTime) && !isJump)
        {
            playerAnimSystem.JumpAnim(); // ジャンプアニメーションを再生する

            moveData.ySpeed = moveData.mainSpeedY;
            jumpTimeCounter += Time.deltaTime;
        }
        // 落下処理（キーを離した、またはジャンプ時間上限に達した）
        else if (!isGrounded)
        {
            
            playerAnimSystem.JumpAnim(); // ジャンプアニメーションを再生する

            isJump = true;
            if (moveData.ySpeed > -moveData.mainSpeedY)
            {
                moveData.ySpeed -= moveData.attenuationJump * Time.deltaTime;
            }
        }

        //----------------------------------------------------[縦移動処理]----------------------------------------------------------

    }

    private void FixedUpdate()
    {
        //物理演算の更新処理
        if(!isSlide)//スライド状態でないとき
            rb.linearVelocity = new Vector2(moveData.xSpeed, moveData.ySpeed);//Rigidbody2Dの速度を更新する
    }
}