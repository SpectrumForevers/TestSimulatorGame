using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string itemName;      // �������� ��������
    public int price;            // ���� ��������
    public string description;    // �������� ��������
    public Sprite icon;          // ������ ��������
    public GameObject model;

    // ����� ��� ��������� ���������� � ��������
    public string GetItemInfo()
    {
        return $"{itemName}\n����: {price}\n��������: {description}";
    }
    
}
