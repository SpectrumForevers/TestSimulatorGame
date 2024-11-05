using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string itemName;      // Название предмета
    public int price;            // Цена предмета
    public string description;    // Описание предмета
    public Sprite icon;          // Иконка предмета
    public GameObject model;

    // Метод для получения информации о предмете
    public string GetItemInfo()
    {
        return $"{itemName}\nЦена: {price}\nОписание: {description}";
    }
    
}
