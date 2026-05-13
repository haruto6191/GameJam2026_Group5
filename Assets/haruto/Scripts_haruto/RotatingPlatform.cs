using UnityEngine;

public class RotatingPlatform : MonoBehaviour
{
    [Header("回転設定")]
    [SerializeField] private float rotationSpeed = 50f; // 回転速度（大きいほど速い）

    void Update()
    {
        // 反時計回りに回転（正の値を指定すると反時計回りになります）
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
    
}