using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour
{
    public static SceneManage sceneManage;

    public void LoadNextScene()
    {
        var currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene + 1);
    }

    public void LoadPreviousScene()
    {
        var currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene - 1);
    }

    public void LoadFirstGameScene()
    {
        SceneManager.LoadScene(0);
    }
}
