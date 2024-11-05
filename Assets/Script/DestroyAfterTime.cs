using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    [Header("Destroy Settings")]
    public float destroyTime = 5f; // ����� � �������� �� �������� �������

    private void Start()
    {
        // �������� ������� ����� �������� �����
        Destroy(gameObject, destroyTime);
    }
}
