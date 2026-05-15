using UnityEngine;
using Spine;
using Unity.VisualScripting;
using Spine.Unity;

public class S_CoinControler : MonoBehaviour
{
    private SkeletonAnimation skel;
    [SerializeField] private ParticleSystem effec;
    [SerializeField] private bool isExtraCoin;

    private UI_General ui;

    private void Start()
    {
        skel = GetComponent<SkeletonAnimation>();
        ui = UI_General.instance;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            effec.Play();
            skel.AnimationState.SetAnimation(0, "Get", false);
             Destroy(gameObject, 0.5f);
        }

        //ÉXÉRÉAëùâ¡èàóùÇ»Ç«
        if(!isExtraCoin)
            ui.GetCoin(1);
        else
        {
            ui.GetCoin(30);
        }
    }
}
