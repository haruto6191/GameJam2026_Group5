using UnityEngine;
using Spine;
using Spine.Unity;

public class S_PlayerAnimSystem : MonoBehaviour
{
    public enum PlayerAnimState//プレイヤーのアニメーション状態を表す列挙型
    {
        Idle,
        Dash,
        Jump,
        Slide,
        FallenDown,
        TakeDamage
    }

    public PlayerAnimState currentAnimState = PlayerAnimState.Idle;//現在のアニメーション状態を保持する変数

    [SerializeField] private SkeletonAnimation skelAnim;//SkeletonAnimationコンポーネントへの参照
    private Skeleton skeleton;
    private SkeletonData skelData;

    //[SerializeField] private ParticleSystem attackEffect;//攻撃エフェクトのParticleSystemへの参照

    private void Start()
    {
        skeleton = skelAnim.Skeleton;
        skelData = skeleton.Data;

        skelAnim.AnimationState.SetAnimation(0, "Idle", true);//0トラックはIdleアニメーションを再生

        skelAnim.AnimationState.Event += OnSpineEvent;
    }

    public void DashAnim()//ダッシュアニメーションを再生するメソッド
    {
        if(currentAnimState == PlayerAnimState.Slide || currentAnimState == PlayerAnimState.FallenDown 
            || currentAnimState == PlayerAnimState.Jump)//現在のアニメーション状態がSlide、FallenDown、Jumpのいずれかの場合
            return;//ダッシュアニメーションを再生せずに終了

        if (currentAnimState != PlayerAnimState.Dash)//現在のアニメーション状態がDashでない場合
        {
            currentAnimState = PlayerAnimState.Dash;//アニメーション状態をDashに変更
            skelAnim.AnimationState.SetAnimation(1, "Dash", true);//1トラックにDashアニメーションを再生
        }
    }

    public void JumpAnim()//ジャンプアニメーションを再生するメソッド
    {
        if(currentAnimState != PlayerAnimState.Jump)//現在のアニメーション状態がJumpでない場合
        {
            currentAnimState = PlayerAnimState.Jump;//アニメーション状態をJumpに変更
            skelAnim.AnimationState.SetAnimation(1, "Jump", true);
        }
    }

    public void SlideAnim()//スライドアニメーションを再生するメソッド
    {
            if(currentAnimState != PlayerAnimState.Slide)//現在のアニメーション状態がSlideでない場合
            {
                currentAnimState = PlayerAnimState.Slide;//アニメーション状態をSlideに変更
                skelAnim.AnimationState.SetAnimation(1, "Slide", false);//1トラックにSlideアニメーションを再生
                //skelAnim.AnimationState.AddEmptyAnimation(1, 0f, 1.2f);//1トラックのアニメーションを3秒かけてフェードアウト
                Invoke("EndSlideAnim", 2f);//2秒後にEndSlideAnimメソッドを呼び出す
        }
    }

    public void FallenDownAnim()//転倒アニメーションを再生するメソッド
    {
        if(currentAnimState != PlayerAnimState.FallenDown)//現在のアニメーション状態がFallenDownでない場合
        {
            currentAnimState = PlayerAnimState.FallenDown;//アニメーション状態をFallenDownに変更
            skelAnim.AnimationState.SetAnimation(1, "FallenDown", false);
        }
    }

    public void IdleAnim()//アイドルアニメーションを再生するメソッド
    {
         if(currentAnimState != PlayerAnimState.Dash)
            return;//現在のアニメーション状態がDashの場合はアイドルアニメーションを再生せずに終了

        if (currentAnimState != PlayerAnimState.Idle)//現在のアニメーション状態がIdleでない場合
            {
                currentAnimState = PlayerAnimState.Idle;//アニメーション状態をIdleに変更
                skelAnim.AnimationState.SetEmptyAnimation(1,0.0f);//1トラックのアニメーションを空にして、0.0秒かけてフェードアウト
                skelAnim.AnimationState.SetAnimation(1, "Idle", true);//1トラックにIdleアニメーションを再生
            }
    }

    public void TakeDamageAnim()//ダメージを受けるアニメーションを再生するメソッド
    {
        if (currentAnimState != PlayerAnimState.TakeDamage)//現在のアニメーション状態がTakeDamageでない場合
        {
            currentAnimState = PlayerAnimState.TakeDamage;//アニメーション状態をTakeDamageに変更
            skelAnim.AnimationState.SetAnimation(1, "TakeDamage", false);//1トラックにTakeDamageアニメーションを再生
            skelAnim.AnimationState.AddEmptyAnimation(1, 0f, 0.5f);//1トラックのアニメーションを0.5秒かけてフェードアウト
        }
    }

    public void AttackAnim()//攻撃アニメーションを再生するメソッド
    {
        if(currentAnimState == PlayerAnimState.Slide || currentAnimState == PlayerAnimState.FallenDown )
                //現在のアニメーション状態がSlide、FallenDownのいずれかの場合
            return;//攻撃アニメーションを再生せずに終了
         skelAnim.AnimationState.SetAnimation(2, "Attack", false);//2トラックにAttackアニメーションを再生
         skelAnim.AnimationState.AddEmptyAnimation(2, 0f, 1.8f);//2トラックのアニメーションを1.8秒かけてフェードアウト
    }

    private void EndSlideAnim()
    {
        currentAnimState = PlayerAnimState.Idle;//アニメーション状態をIdleに変更
    }

    public void EmptyAnim()
    {
               skelAnim.AnimationState.SetEmptyAnimation(1, 0f);//1トラックのアニメーションを空にして、0秒かけてフェードアウト
    }

    private void OnSpineEvent(TrackEntry trackEntry, Spine.Event e)//Spineイベントが発生したときに呼び出されるメソッド
    {
        Debug.Log("Spine Event: " + e.Data.Name);//イベントの名前をログに出力

        if (e.Data.Name == "AttackIEvent")//イベントの名前が"AttackEffect"の場合
        {

            Debug.Log("AttackEffect Event Triggered!");//攻撃エフェクトイベントがトリガーされたことをログに出力
            //attackEffect.Play();//攻撃エフェクトのParticleSystemを再生
        }
    }

}
