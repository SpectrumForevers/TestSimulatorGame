using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    public float holdDistance = 2f; // ����������, �� ������� ����� ���������� ������ �� ������
    private GameObject selectedObject; // ������, ������� �� ����������
    [SerializeField] bool isHolding = false; // ����, �����������, ������ �� ������
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

        // �������� �� ������� ������� E ��� �������� � ���������� �������
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!isHolding)
            {
                if (Physics.Raycast(ray, out hit, 2f))
                {
                    selectedObject = hit.collider.gameObject;
                    isHolding = true;
                    rb.isKinematic = true; // ��������� ������ ��� �������, ����� ��������� �� �������
                }
            }
            else
            {
                selectedObject = null;
                isHolding = false;
                rb.isKinematic = false; // �������� ������ �������
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
        // �������� �� ������� �������
        if (isHolding && selectedObject != null)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                {
                    // ������� �� 90 ��������, ���� ������ ������� Shift
                    selectedObject.transform.Rotate(0f, 90f, 0f, Space.Self);
                }
                else
                {
                    // ������� �� 45 ��������, ���� ������� Shift �� ������
                    selectedObject.transform.Rotate(0f, 45f, 0f, Space.Self);
                }
            }

            // ����������� �������
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
