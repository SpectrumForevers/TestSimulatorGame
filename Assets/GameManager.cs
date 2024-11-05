using UnityEngine;
using UnityEngine.SceneManagement; 

public class SceneSwitcher : MonoBehaviour
{
    void Update()
    {
        // Если нажата клавиша Escape (ESC)
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None; // Разблокировать курсор
            Cursor.visible = true; // Сделать курсор видимым
            // Загружаем сцену с индексом 0 (первая сцена в списке)
            SceneManager.LoadScene(0);
        }
    }
}
