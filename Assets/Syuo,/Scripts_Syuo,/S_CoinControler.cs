using UnityEngine;
using Spine;
using Unity.VisualScripting;
using Spine.Unity;

public class S_CoinControler : MonoBehaviour
{
    private SkeletonAnimation skel;
    [SerializeField] private ParticleSystem effec;

    private void Start()
    {
        skel = GetComponent<SkeletonAnimation>();
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
    }
}
