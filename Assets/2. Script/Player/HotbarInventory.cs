using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HotbarInventory : MonoBehaviour
{
    public Item scanner; // ������ ���� ������ ����� ��������
    public Item[] slots = new Item[3]; // 3 ����� ��� ���������
    public Image[] slotImages; // UI Images ��� ����������� ���������
    public float interactionDistance = 2f; // ������ ��� ��������������

    private int selectedSlotIndex = 0; // ������ ���������� �����
    private float holdTime = 0f;
    bool scanerActiv = false;

    [SerializeField] TMP_Text namePhone;
    [SerializeField] TMP_Text price;
    void Start()
    {
        slots[0] = scanner;
        UpdateSlotImages();
    }

    void Update()
    {
        HandleInput();
        if (Input.GetKey(KeyCode.E))
        {
            holdTime += Time.deltaTime; 
        }
        else
        {
            holdTime = 0f; 
        }
        if (holdTime > 0.2f)
        {
            TryPickUpItem();
        }
        
        
    }

    void HandleInput()
    {

        foreach (Image sprite in slotImages)
        {
            if (slotImages[selectedSlotIndex] == sprite)
            {
                sprite.color = Color.green;
            }
            else
            {
                sprite.color = Color.white;
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedSlotIndex = 0;
            
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            selectedSlotIndex = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            selectedSlotIndex = 2;
        }

        if (selectedSlotIndex == 0 && Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log($"������ �����������. �������� ���� 2 ��� 3.");
            scanerActiv = true;
        }
        if ((slots[selectedSlotIndex] != null && selectedSlotIndex != 0) && scanerActiv == true)
        {
            Debug.Log(slots[selectedSlotIndex].GetItemInfo());
            namePhone.text = slots[selectedSlotIndex].itemName;
            price.text = slots[selectedSlotIndex].price.ToString();
            scanerActiv = false;

        }

        if (Input.GetKeyDown(KeyCode.Q) && selectedSlotIndex > 0 && slots[selectedSlotIndex] != null)
        {
            DropItem(selectedSlotIndex);
        }
        if (scanerActiv == true)
        {
            slotImages[0].color = Color.blue;
        }
    }

    void TryPickUpItem()
    {
        // ��������� ��� ������� � �������
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, interactionDistance);

        foreach (var hitCollider in hitColliders)
        {
            ItemPickup itemPickup = hitCollider.GetComponent<ItemPickup>();
            if (itemPickup != null)
            {
                PickUpItem(itemPickup.item);
                Destroy(hitCollider.gameObject);
                break;
            }
        }
    }

    void PickUpItem(Item item)
    {
        for (int i = 1; i < slots.Length; i++)
        {
            if (slots[i] == null) 
            {
                slots[i] = item; 
                UpdateSlotImages(); 
                Debug.Log($"�������� �������: {item.GetItemInfo()}");
                return;
            }
        }
        Debug.Log("��� ��������� ������ ��� ������� ��������.");
    }
    void DropItem(int index)
    {
        Item itemToDrop = slots[index];
        if (itemToDrop != null)
        {
            Debug.Log($"������� '{itemToDrop.itemName}' ��������.");

            Vector3 cameraPosition = Camera.main.transform.position; 
            Vector3 forwardDirection = Camera.main.transform.forward; 


            Vector3 dropPosition = cameraPosition + forwardDirection * 2f;

            RaycastHit hit;
            if (Physics.Raycast(cameraPosition, forwardDirection, out hit, 2f))
            {
                dropPosition = hit.point;
            }

            Debug.DrawLine(cameraPosition, dropPosition, Color.red, 2f); 

            GameObject itemObject = Instantiate(itemToDrop.model, dropPosition, Quaternion.identity); 
            ItemPickup itemPickupComponent = itemObject.AddComponent<ItemPickup>();
            itemPickupComponent.item = itemToDrop; 

            slots[index] = null; 
            UpdateSlotImages(); 
        }
    }

    void UpdateSlotImages()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i] != null && slotImages[i] != null)
            {
                slotImages[i].sprite = slots[i].icon; 
                slotImages[i].enabled = true; 
            }
            else if (slotImages[i] != null)
            {
                slotImages[i].sprite = null;
                slotImages[i].enabled = false;
            }
        }
    }
}
