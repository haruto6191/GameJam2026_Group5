using UnityEngine;
using Spine;
using Unity.VisualScripting;
using Spine.Unity;

public class S_CoinControler : MonoBehaviour
{
    [SerializeField] private SkeletonAnimation skel;
    [SerializeField] private ParticleSystem effec;
    [SerializeField] private bool isExtraCoin;
    [SerializeField] private GameObject coinObj;
    [SerializeField] private int ExtraCoinNo = 0;
    private bool isGet;

    private Exp_player_status stat;

    private UI_General ui;

    private void Start()
    {
        ui = UI_General.instance;
        stat = Exp_player_status.instance;
        isGet = false;
        coinObj.SetActive(true);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //ÉXÉRÉAëùâ¡èàóùÇ»Ç«
        if (!isExtraCoin && !isGet)
            ui.GetCoin(1);
        else if(!isGet)
        {
            ui.GetExCoin(ExtraCoinNo);
        }

        if (collision.CompareTag("Player") && !isGet)
        {
            effec.Play();
            skel.AnimationState.SetAnimation(0, "Get", false);
            
            isGet = true;
            Invoke("ActiveItem", 0.5f);
        }

        
    }

    private void ActiveItem()
    {
        coinObj.SetActive(false);
    }

    private void Update()
    {
        if(stat.isDead && isGet)
        {
            coinObj.SetActive(true);
            isGet = false;
            skel.AnimationState.SetAnimation(0, "Default", true);
        }        
    }
}
