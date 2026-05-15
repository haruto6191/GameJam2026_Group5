using UnityEngine;
using System.Collections;

public class TrapFloor : MonoBehaviour
{
    [Header("設定")]
    [SerializeField] private float trapDelay = 0.5f;    // 抜けるまでの時間
    [SerializeField] private float resetDelay = 2.0f;   // 復活するまでの時間
    [SerializeField] private float shakeMagnitude = 0.05f; // 振動の強さ

    private BoxCollider2D col;
    private SpriteRenderer sr;
    private Color originalColor;
    private bool isTriggered = false;

    void Start()
    {
        col = GetComponent<BoxCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isTriggered)
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                if (contact.normal.y < -0.5f)
                {
                    StartCoroutine(TrapRoutine());
                    break;
                }
            }
        }
    }

    private IEnumerator TrapRoutine()
    {
        isTriggered = true;

        // 色を変化させる
        sr.color = Color.red;

        //振動する処理
        float elapsed = 0f;
        Vector3 originalPos = transform.position;

        while (elapsed < trapDelay)
        {
            // 元の位置を中心に、ランダムに少しだけずらす
            transform.position = originalPos + (Vector3)Random.insideUnitCircle * shakeMagnitude;

            elapsed += Time.deltaTime;
            yield return null;
        }

        // 振動が終わったら位置を正確に戻す
        transform.position = originalPos;

        // 底が抜ける処理
        col.enabled = false;
        sr.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0.3f);

        // 復活させる
        yield return new WaitForSeconds(resetDelay);
        col.enabled = true;
        sr.color = originalColor;
        isTriggered = false;
    }
}