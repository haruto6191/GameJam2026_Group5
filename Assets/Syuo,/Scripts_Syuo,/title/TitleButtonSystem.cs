using UnityEngine;

public class TitleButtonSystem : MonoBehaviour
{

    [SerializeField] private string sceneName;

    public void QuitGame()
    {
               Application.Quit();
    }

    public void StartGame()
    {
        if(Time.timeScale != 1) Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}
