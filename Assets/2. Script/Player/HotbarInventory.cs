using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HotbarInventory : MonoBehaviour
{
    public Item scanner; // Первый слот всегда занят сканером
    public Item[] slots = new Item[3]; // 3 слота для предметов
    public Image[] slotImages; // UI Images для отображения предметов
    public float interactionDistance = 2f; // Радиус для взаимодействия

    private int selectedSlotIndex = 0; // Индекс выбранного слота
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
            Debug.Log($"Сканер активирован. Выберите слот 2 или 3.");
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
        // Считываем все объекты в радиусе
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
                Debug.Log($"Подобран предмет: {item.GetItemInfo()}");
                return;
            }
        }
        Debug.Log("Нет свободных слотов для подбора предмета.");
    }
    void DropItem(int index)
    {
        Item itemToDrop = slots[index];
        if (itemToDrop != null)
        {
            Debug.Log($"Предмет '{itemToDrop.itemName}' выброшен.");

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
