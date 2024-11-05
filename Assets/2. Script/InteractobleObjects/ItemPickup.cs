using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item item; // Ссылка на предмет (ScriptableObject)

    void OnDrawGizmos()
    {
        // Визуализируем радиус взаимодействия
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 2f); // Радиус 2 единицы
    }

    // Метод для создания нового объекта на сцене на основе ScriptableObject
    public GameObject CreateItemObject()
    {
        GameObject itemObject = Instantiate(item.model, transform.position, Quaternion.identity); // Используем модель предмета
        itemObject.AddComponent<ItemPickup>().item = item; // Добавляем компонент ItemPickup с ссылкой на ScriptableObject
        itemObject.AddComponent<BoxCollider>(); // Добавьте соответствующий коллайдер
        itemObject.AddComponent<Rigidbody>().isKinematic = true; // Убедитесь, что объект не подвержен физике сразу
        return itemObject; // Возвращаем созданный объект
    }
}
