using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("基本移動")]
    [SerializeField] private float moveSpeed = 8f;      // 通常の移動速度
    [SerializeField] private float dashMultiplier = 1.5f; // ダッシュ時の倍率
    [SerializeField] private float jumpForce = 12f;     // ジャンプ力
    [SerializeField] private int extraJumpsValue = 1;   // 空中ジャンプできる回数

    [Header("スライディング設定")]
    [SerializeField] private float slideSpeed = 15f;    // スライディング速度
    [SerializeField] private float slideDuration = 0.5f; // スライディングの持続時間
    [SerializeField] private float slideColliderHeight = 0.5f; // スライディング中のコライダーの高さ

    [Header("接地判定の設定")]
    [SerializeField] private Transform groundCheck;    // 足元の判定用オブジェクト
    [SerializeField] private float checkRadius = 0.2f; // 接地判定の半径
    [SerializeField] private LayerMask groundLayer;    // 地面として扱うレイヤー

    // 内部コンポーネント用
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;

    // 状態管理用変数
    private float moveInput;
    private bool isGrounded;
    private int extraJumps;
    private bool isFacingRight = true;
    private bool isDashing;

    // スライディング内部管理
    private bool isSliding;
    private float slideTimer;
    private float originalColliderHeight;
    private Vector2 originalColliderOffset;

    void Start()
    {
        // コンポーネントの取得
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();

        // 初期設定
        extraJumps = extraJumpsValue;

        // スライディング終了後に戻せるよう、元のコライダーのサイズを保存
        if (boxCollider != null)
        {
            originalColliderHeight = boxCollider.size.y;
            originalColliderOffset = boxCollider.offset;
        }
    }

    void Update()
    {
        // --- 1. スライディング中の処理 ---
        if (isSliding)
        {
            HandleSlideUpdate();
            return; // スライディング中は以下の入力を受け付けない
        }

        // --- 2. 入力の取得 ---
        moveInput = Input.GetAxisRaw("Horizontal"); // A/D または 左右キー
        isDashing = Input.GetKey(KeyCode.LeftShift); // Shiftキーでダッシュ

        // --- 3. 接地判定とジャンプ回数リセット ---
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);
        if (isGrounded)
        {
            extraJumps = extraJumpsValue;
        }

        // --- 4. ジャンプ処理 ---
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                Jump();
            }
            else if (extraJumps > 0)
            {
                Jump();
                extraJumps--;
            }
        }

        // --- 5. スライディング開始の判定 ---
        // 接地している、かつ左Ctrlキーが押されたら発動
        if (Input.GetKeyDown(KeyCode.LeftControl) && isGrounded && !isSliding)
        {
            StartSlide();
        }

        // --- 6. 向きの反転 ---
        if ((moveInput > 0 && !isFacingRight) || (moveInput < 0 && isFacingRight))
        {
            Flip();
        }
    }

    void FixedUpdate()
    {
        // スライディング中はFixedUpdateでの移動計算をスキップ（Update側で制御するため）
        if (isSliding) return;

        // 現在の速度（ダッシュ中かどうかで切り替え）
        float currentSpeed = isDashing ? moveSpeed * dashMultiplier : moveSpeed;

        // 速度の適用
        rb.linearVelocity = new Vector2(moveInput * currentSpeed, rb.linearVelocity.y);
    }

    // --- ジャンプ関数 ---
    void Jump()
    {
        // 落下中でも一定の高さまで飛べるように速度を上書き
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }

    // --- スライディング開始 ---
    void StartSlide()
    {
        isSliding = true;
        slideTimer = slideDuration;

        // コライダーを低くする
        boxCollider.size = new Vector2(boxCollider.size.x, slideColliderHeight);

        // 足元の位置を固定するため、高さが減った分だけオフセットを下げる
        float heightDifference = originalColliderHeight - slideColliderHeight;
        boxCollider.offset = new Vector2(originalColliderOffset.x, originalColliderOffset.y - (heightDifference / 2f));

        // 向いている方向に力を加える
        float direction = isFacingRight ? 1f : -1f;
        rb.linearVelocity = new Vector2(direction * slideSpeed, rb.linearVelocity.y);
    }

    // --- スライディング中の更新 ---
    void HandleSlideUpdate()
    {
        slideTimer -= Time.deltaTime;

        // 向いている方向に一定の速度を維持
        float direction = isFacingRight ? 1f : -1f;
        rb.linearVelocity = new Vector2(direction * slideSpeed, rb.linearVelocity.y);

        // 時間が経過したら終了
        if (slideTimer <= 0)
        {
            StopSlide();
        }

        // スライディング中にジャンプが押されたらキャンセルして跳ぶ
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StopSlide();
            Jump();
        }
    }

    // --- スライディング終了 ---
    void StopSlide()
    {
        isSliding = false;

        // コライダーを元のサイズに戻す
        boxCollider.size = new Vector2(boxCollider.size.x, originalColliderHeight);
        boxCollider.offset = originalColliderOffset;
    }

    // --- 左右反転 ---
    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
}