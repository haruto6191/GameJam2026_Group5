using UnityEngine;

[CreateAssetMenu(fileName = "Score_Manager", menuName = "Scriptable Objects/Score_Manager")]
public class Score_Manager : ScriptableObject
{
    public int score, coin, life, player_HP;
    public float time;
}
