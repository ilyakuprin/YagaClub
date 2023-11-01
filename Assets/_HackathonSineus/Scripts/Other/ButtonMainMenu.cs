using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonMainMenu : MonoBehaviour
{
    public void OnNextScene()
    {
        var indexCurrentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(indexCurrentScene + 1);
    }

    public void OnExit()
    {
        Application.Quit();
    }
}
