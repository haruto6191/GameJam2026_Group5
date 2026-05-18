using UnityEngine;

public class Scene_loader : MonoBehaviour
{
    [SerializeField] Game_Clear_SC Game_Clear;

    //public bool Reset_Restart;

    //public Score_Manager manager;

    void FixedUpdate()
    {
        //特定のボタンを押して移動するならここー
        /*
        if()
            LoadScene();
        */
    }

    public void LoadScene()
    {
        Game_Clear.GameClear();
        //SceneManager.LoadScene(sceneName);
    }
    /*
    public void DataAdd()
    {
        if (Reset_Restart)
        {
            manager.time = 300;
            manager.score = 0;
            manager.coin = 0;
            manager.life = 3;

            manager.player_HP = 100;
        }
        else
        {
            manager.time = UI_General.instance.game_time;
            manager.score = UI_General.instance.score;
            manager.coin = UI_General.instance.coin;
            manager.life = UI_General.instance.life;

            manager.player_HP = Exp_player_status.instance.player_HP;
        }
        LoadScene();
    }
    */
    //プレイヤーがこれをアタッチしたオブジェクトに触れた瞬間
    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.tag == "Player")
        {
            //DataAdd();
            LoadScene();
        }
    }
}
