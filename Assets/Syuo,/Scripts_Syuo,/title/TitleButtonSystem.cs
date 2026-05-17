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
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}
