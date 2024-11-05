using UnityEngine;

public class CameraBob : MonoBehaviour
{
    [Header("Bob Settings")]
    public float bobFrequency = 5f;        // Частота покачивания
    public float bobHorizontalAmplitude = 0.05f; // Амплитуда покачивания влево-вправо
    public float bobVerticalAmplitude = 0.1f;    // Амплитуда покачивания вверх-вниз
    public float transitionSpeed = 5f;     // Скорость перехода при старте и остановке ходьбы

    private float bobTimer = 0f;
    private Vector3 initialPosition;
    private bool isMoving = false;

    private void Start()
    {
        initialPosition = transform.localPosition;
    }

    private void Update()
    {
        CheckMovement();
        ApplyCameraBob();
    }

    private void CheckMovement()
    {
        
        isMoving = Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0;
    }

    private void ApplyCameraBob()
    {
        if (isMoving)
        {
            
            bobTimer += Time.deltaTime * bobFrequency;


            float horizontalOffset = Mathf.Sin(bobTimer) * bobHorizontalAmplitude;
            float verticalOffset = Mathf.Cos(bobTimer * 2) * bobVerticalAmplitude;

            transform.localPosition = Vector3.Lerp(transform.localPosition,
                                                   initialPosition + new Vector3(horizontalOffset, verticalOffset, 0),
                                                   Time.deltaTime * transitionSpeed);
        }
        else
        {

            transform.localPosition = Vector3.Lerp(transform.localPosition, initialPosition, Time.deltaTime * transitionSpeed);
            bobTimer = 0f;
        }
    }
}
