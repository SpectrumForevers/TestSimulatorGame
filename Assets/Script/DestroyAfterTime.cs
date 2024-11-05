using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    [Header("Destroy Settings")]
    public float destroyTime = 5f; // Время в секундах до удаления объекта

    private void Start()
    {
        // Удаление объекта через заданное время
        Destroy(gameObject, destroyTime);
    }
}
