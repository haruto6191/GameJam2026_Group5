using UnityEngine;
using Spine;
using Spine.Unity;
using System.Collections;

public class S_PlayerAnimSystem : MonoBehaviour
{
    public enum PlayerAnimState//プレイヤーのアニメーション状態を表す列挙型
    {
        Idle,
        Dash,
        Jump,
        Slide,
        FallenDown,
        TakeDamage,
        Squat
    }

    public PlayerAnimState currentAnimState = PlayerAnimState.Idle;//現在のアニメーション状態を保持する変数

    [SerializeField] private SkeletonAnimation skelAnim;//SkeletonAnimationコンポーネントへの参照
    private Skeleton skeleton;
    private SkeletonData skelData;


    [SerializeField] private GameObject slash;
    //[SerializeField] private ParticleSystem attackEffect;//攻撃エフェクトのParticleSystemへの参照

    [SerializeField] private float slashDuration = 0.5f;//攻撃エフェクトの持続時間
    [SerializeField] private float slashSpeed = 0.1f;//攻撃エフェクトの移動速度

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

    public void SquatAnim()//しゃがみアニメーションを再生するメソッド
    {
        if (currentAnimState == PlayerAnimState.Slide || currentAnimState == PlayerAnimState.FallenDown
            || currentAnimState == PlayerAnimState.Jump)//現在のアニメーション状態がSlide、FallenDown、Jumpのいずれかの場合
            return;//ダッシュアニメーションを再生せずに終了
        if (currentAnimState != PlayerAnimState.Squat)//現在のアニメーション状態がSquatでない場合
        {
            currentAnimState = PlayerAnimState.Squat;//アニメーション状態をSquatに変更
            skelAnim.AnimationState.SetAnimation(1, "Squat", false);//1トラックにSquatアニメーションを再生
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

        if (e.Data.Name == "AttackIEvent" && currentAnimState == PlayerAnimState.Squat)//イベントの名前が"AttackEffect"の場合
        {
            GameObject _slash = Instantiate(slash, transform.position, Quaternion.identity);//攻撃エフェクトのプレハブを生成
            bool isLeft = transform.localScale.x < 0;//プレイヤーの向きが左かどうかを判定
            if (transform.localScale.x < 0)//プレイヤーの向きが左の場合
            {
                _slash.transform.localScale = new Vector3(-1, 1, 1);//攻撃エフェクトの向きを反転
            }
            StartCoroutine(SlashCourtine(_slash,isLeft));

        }
    }

    private IEnumerator SlashCourtine(GameObject sl,bool isLeft)
    {
        sl.transform.position = new Vector3(sl.transform.position.x, sl.transform.position.y + 0.8f, sl.transform.position.z);
        //攻撃エフェクトの位置をプレイヤーの位置から少し上にずらす

        float time = 0f;//経過時間を格納する変数
        while (time < slashDuration)
        {
            time += Time.deltaTime;//経過時間を更新

            float currentSpeed = slashSpeed * Time.deltaTime;//攻撃エフェクトの移動速度をフレームレートに依存しないようにするために、Time.deltaTimeを掛ける

            if (isLeft)//プレイヤーの向きが左の場合
            {
                sl.transform.position += new Vector3(-currentSpeed, 0, 0);//攻撃エフェクトを左に移動
            }
            else//プレイヤーの向きが右の場合
            {
                sl.transform.position += new Vector3(currentSpeed, 0, 0);//攻撃エフェクトを右に移動
            }

           // Debug.Log("攻撃エフェクトの位置: " + sl.transform.position);//攻撃エフェクトの位置をログに出力
            yield return null;//次のフレームまで待つ
        }

        //Debug.Log("攻撃エフェクトを破壊します。");//攻撃エフェクトを破壊する前にログを出力
        Destroy(sl);//攻撃エフェクトを破壊する
    }


}
