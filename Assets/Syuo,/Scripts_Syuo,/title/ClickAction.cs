using Spine.Unity;
using UnityEngine;

public class ClickAction : MonoBehaviour
{
    [SerializeField]
    private SkeletonAnimation skelAnim;


    private void OnMouseDown()
    {
        if(skelAnim != null)
        {
            skelAnim.AnimationState.SetAnimation(1, "Touch", false);
            skelAnim.AnimationState.AddEmptyAnimation(1, 0.4f, 0);
        }
    }
}
