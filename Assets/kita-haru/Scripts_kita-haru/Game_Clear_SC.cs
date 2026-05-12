using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game_Clear_SC : MonoBehaviour
{
    [SerializeField] GameObject Hidden_Can, GC_text, GC_Can, title_bt;
    [SerializeField] Text sc_text, plus_ef, plus_txt;

    private string[] general_str = {"TIME","COIN","LIFE"};
    private int[] general_data = new int[3];

    public void GameClear()
    {
        Time.timeScale = 0; //プレイヤーが敵の最後の攻撃でやられないために一応止める

        //for文で回しやすくするため、配列にそれぞれの値を格納
        general_data[0] = (int)UI_General.instance.game_time * 10;
        general_data[1] = UI_General.instance.coin * 10;
        general_data[2] = UI_General.instance.life * 100;

        //以下のコルーチンスタート
        StartCoroutine(GameClearCoroutine());
    }

    IEnumerator GameClearCoroutine()
    {
        //GameClearのキャンバスを表示
        GC_Can.SetActive(true);
        //Canvasを非表示に
        Hidden_Can.SetActive(false);

        yield return new WaitForSecondsRealtime(0.5f);
        //以下はまあ、テキストの演出を適当に組んだだけだから気にせずに
        GC_text.SetActive(true);

        yield return new WaitForSecondsRealtime(0.5f);

        sc_text.text = UI_General.instance.score.ToString("D6");
        sc_text.gameObject.SetActive(true);

        yield return new WaitForSecondsRealtime(1.0f);

        for (int i = 0; i < 3; i++)
        {
            plus_ef.gameObject.SetActive(true);

            plus_txt.text = general_str[i];
            plus_ef.text = general_data[i].ToString();

            yield return new WaitForSecondsRealtime(1.5f);

            plus_ef.gameObject.SetActive(false);

            UI_General.instance.score += general_data[i];
            sc_text.text = UI_General.instance.score.ToString("D6");

            yield return new WaitForSecondsRealtime(1.0f);
        }

        title_bt.SetActive(true);
    }

    //タイトルシーンに行くやーつ
    public void GotoTitle()
    {
        /*
        Time.timeScale = 1.0f;

        SceneManager.LoadScene("Title_Scene");
        */
    }
}
