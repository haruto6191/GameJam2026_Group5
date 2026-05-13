using UnityEngine;

public class WarpPortal : MonoBehaviour
{
    [Header("ワープ先の設定")]
    [SerializeField] private Transform destination; // ワープ先の座標を持つオブジェクト
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            // プレイヤーの座標を、目的地の座標に書き換える
            // destination.position で目的地の中心座標を取得
            collision.transform.position = destination.position;

           // Debug.Log("Teleported to " + destination.name);
        }
    }
}