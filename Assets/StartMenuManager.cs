
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenuManager : MonoBehaviour
{
    [SerializeField] Button play;
    [SerializeField] Button exit;

    private void Awake()
    {
        play.GetComponent<Button>().onClick.AddListener(LoadNextScene);
        exit.GetComponent<Button>().onClick.AddListener(StopGame);
    }

    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("Нет следующей сцены для загрузки.");
        }
    }
    public void StopGame()
    {
        Application.Quit();
    }
}
