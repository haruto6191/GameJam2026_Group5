using UnityEngine;

public class Appearance_Sys : MonoBehaviour
{
    [SerializeField] GameObject App;

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.tag == "Player")
        {
            App.gameObject.SetActive(true);
        }
    }
}
