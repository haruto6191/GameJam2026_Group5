using UnityEngine;

public class BottomlessSwamp : MonoBehaviour
{
    [Header("沼の設定")]
    [SerializeField] private float sinkSpeed = 2.0f; // 沈む速さ

    /*private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            S_PlayerMove player = other.GetComponent<S_PlayerMove>();

            if (player != null)
            {
                // S_PlayerMoveと同じキー（A, D, Space）で入力を判定
                bool isInputting = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.Space);

                if (!isInputting)
                {
                    // 操作していない場合：
                    // Playerスクリプトの public変数「ySpeed」を直接書き換えて沈ませる
                    player.moveData.ySpeed = -sinkSpeed;
                }
                else
                {
                    // 操作している場合：
                    // 普通の落下速度よりも速く落ちないように制限をかける
                    if (player.moveData.ySpeed < -sinkSpeed)
                    {
                        player.moveData.ySpeed = -sinkSpeed;
                    }
                }
            }
        }
    }*/
}