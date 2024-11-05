using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    public float holdDistance = 2f; // Расстояние, на котором будет находиться объект от камеры
    private GameObject selectedObject; // Объект, который мы перемещаем
    [SerializeField] bool isHolding = false; // Флаг, указывающий, поднят ли объект
    private Rigidbody rb;
    bool focus = false;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        RaycastHit hit;

        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

        // Проверка на нажатие клавиши E для поднятия и отпускания объекта
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!isHolding)
            {
                if (Physics.Raycast(ray, out hit, 2f))
                {
                    selectedObject = hit.collider.gameObject;
                    isHolding = true;
                    rb.isKinematic = true; // Отключаем физику для объекта, чтобы управлять им вручную
                }
            }
            else
            {
                selectedObject = null;
                isHolding = false;
                rb.isKinematic = false; // Включаем физику обратно
            }
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            focus = true;
        }
        if (Input.GetKeyUp(KeyCode.V))
        {
            focus = false;
        }
        // Проверка на поворот объекта
        if (isHolding && selectedObject != null)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                {
                    // Поворот на 90 градусов, если зажата клавиша Shift
                    selectedObject.transform.Rotate(0f, 90f, 0f, Space.Self);
                }
                else
                {
                    // Поворот на 45 градусов, если клавиша Shift не зажата
                    selectedObject.transform.Rotate(0f, 45f, 0f, Space.Self);
                }
            }

            // Перемещение объекта
            if(focus == false)
                MoveSelectedObject(holdDistance);
            else
            {
                MoveSelectedObject(holdDistance / 1.2f);
            }
        }
    }

    void MoveSelectedObject(float holdDistance)
    {
        
        Vector3 targetPosition = Camera.main.transform.position + Camera.main.transform.forward * holdDistance;

        
        selectedObject.transform.position = Vector3.Lerp(selectedObject.transform.position, targetPosition, Time.deltaTime * 10f);
    }
}
