using UnityEngine;
using UnityEngine.UI;

public class UI_General : MonoBehaviour
{
    public static UI_General instance; //ƒOƒ[ƒoƒ‹‰»

    public int score = 0, coin = 0, life = 3;
    public float game_time = 300;

    [SerializeField]
    Text score_text, coin_text, life_text, time_text;

    public void Awake()
    {
        if (instance == null) instance = this;
    }

    void FixedUpdate()
    {
        //timeŒ¸Z
        game_time -= Time.deltaTime;

        if(game_time <= 0) //‚à‚µtime‚ª0ˆÈ‰º‚É‚È‚Á‚½‚ç
        {
            //time up -> game over
        }
        else //ƒ}ƒCƒiƒX•\‹L–h~
        {
            //time_text‚É”½‰f
            time_text.text = game_time.ToString("F0");
        }
    }

    public void GetScore(int s) //scoreŠl“¾
    {
        //‰ÁZ
        score += s;
        
        //score_text‚É”½‰f
        score_text.text = score.ToString("D6");
    }

    public void GetCoin(int c) //coinŠl“¾
    {
        //‰ÁZ
        coin += c;

        if(coin >= 100) //‚à‚µcoin‚ğ100–‡ˆÈãŠl“¾‚µ‚½‚ç
        {
            //Œ¸Z
            coin -= 100;

            //c‹@‚ğ +1 ‚·‚é
            GetLife(1);
        }

        //coin_text‚É”½‰f
        coin_text.text = coin.ToString();
    }

    public void GetLife(int l) //lifeŠl“¾
    {
        //‰ÁZ
        life += l;

        if (life >= 100) //‚à‚µlife‚ª100‚ğ’´‚¦‚½‚ç
        {
            int sa = life - 99; //·•ª‚ğŠm•Û

            GetScore(sa * 10000); //·•ª * 10000‚ğƒXƒRƒA‚É•ÏŠ·

            life = 99; //99‚ÉŒÅ’è
        }

        //life_text‚É”½‰f
        life_text.text = life.ToString();
    }
}
