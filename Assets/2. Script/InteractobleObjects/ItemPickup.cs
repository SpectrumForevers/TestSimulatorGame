using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item item; // ������ �� ������� (ScriptableObject)

    void OnDrawGizmos()
    {
        // ������������� ������ ��������������
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 2f); // ������ 2 �������
    }

    // ����� ��� �������� ������ ������� �� ����� �� ������ ScriptableObject
    public GameObject CreateItemObject()
    {
        GameObject itemObject = Instantiate(item.model, transform.position, Quaternion.identity); // ���������� ������ ��������
        itemObject.AddComponent<ItemPickup>().item = item; // ��������� ��������� ItemPickup � ������� �� ScriptableObject
        itemObject.AddComponent<BoxCollider>(); // �������� ��������������� ���������
        itemObject.AddComponent<Rigidbody>().isKinematic = true; // ���������, ��� ������ �� ��������� ������ �����
        return itemObject; // ���������� ��������� ������
    }
}
