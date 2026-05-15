using UnityEngine;

public class S_AttackSystem : MonoBehaviour
{

    [SerializeField] private float attackInterval = 3f;//攻撃のインターバル時間
    [SerializeField] private float jumpAttackLevitation = 10f;//ジャンプ攻撃の浮遊力
    private float lastAttackTime = 0f;//最後の攻撃時間
    private bool isAttacking = false;//攻撃中かどうか  
    private S_PlayerAnimSystem playerAnimSystem;//S_PlayerAnimSystemコンポーネントへの参照
    private S_PlayerMove playerMove;//S_PlayerMoveコンポーネントへの参照

    private void Start()
    {
        playerAnimSystem = GetComponent<S_PlayerAnimSystem>();//S_PlayerAnimSystemコンポーネントを取得
        playerMove = GetComponent<S_PlayerMove>();//S_PlayerMoveコンポーネントを取
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.K) && !isAttacking)//Kキーを押したとき
        {
            playerAnimSystem.AttackAnim();//攻撃アニメーションを再生
            isAttacking = true;//攻撃中にする
            lastAttackTime = 0;

            if(!playerMove.isGrounded)
            {
                playerMove.moveData.ySpeed = jumpAttackLevitation;//ジャンプ攻撃の浮遊力を設定する
            }
        }

        if(lastAttackTime >= attackInterval)//最後の攻撃からインターバル時間が経過している場合
        {
            isAttacking = false;//攻撃を終了する
        }

        lastAttackTime += Time.deltaTime;//最後の攻撃時間を更新する
    }
}
