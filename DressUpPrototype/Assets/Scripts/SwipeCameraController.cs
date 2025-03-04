using UnityEngine;

public class SwipeCameraController : MonoBehaviour
{
    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;
    private bool isSwiping = false;
    private Vector3 targetPosition; // Целевая позиция камеры
    [SerializeField] private float swipeSensitivity = 0.01f; // Чувствительность свайпа
    [SerializeField] private float lerpSpeed = 0.5f; // Скорость сглаживания движения

    private void Start()
    {
        targetPosition = transform.position; // Изначально цель = текущая позиция камеры
    }

    private void Update()
    {
        DetectSwipeMouse();

        // Плавное движение камеры
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * lerpSpeed);
    }

    private void DetectSwipeMouse()
    {
        if (Input.GetMouseButtonDown(0)) // Начало свайпа
        {
            startTouchPosition = Input.mousePosition;
            targetPosition = Camera.main.transform.position;
            transform.position = targetPosition;
            isSwiping = true;

            Vector2 mousePosition = startTouchPosition;

            mousePosition = Camera.main.ScreenToWorldPoint(startTouchPosition);

            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider?.gameObject.name != "Background")
            {
                isSwiping = false;
                return;
            }

        }
        if (Input.GetMouseButtonUp(0)) // Конец свайпа
        {
            if (isSwiping)
            {
                endTouchPosition = Input.mousePosition;
                Vector2 swipeVector = endTouchPosition - startTouchPosition;

                MoveCamera(-swipeVector); // Инверсия направления
            }
            isSwiping = false;
        }
    }

    private void MoveCamera(Vector2 direction)
    {
        Vector3 moveDirection = new Vector3(direction.x, 0, 0) * swipeSensitivity;
        targetPosition += moveDirection; // Обновляем целевую позицию
    }
}
