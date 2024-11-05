using UnityEngine;
using UnityEngine.SceneManagement; 

public class SceneSwitcher : MonoBehaviour
{
    void Update()
    {
        // ���� ������ ������� Escape (ESC)
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None; // �������������� ������
            Cursor.visible = true; // ������� ������ �������
            // ��������� ����� � �������� 0 (������ ����� � ������)
            SceneManager.LoadScene(0);
        }
    }
}
