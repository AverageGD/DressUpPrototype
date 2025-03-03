using UnityEngine;

public class SwipeCameraController : MonoBehaviour
{
    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;
    private bool isSwiping = false;
    private Vector3 targetPosition; // Целевая позиция камеры
    public float swipeSensitivity = 0.1f; // Чувствительность свайпа
    public float lerpSpeed = 5f; // Скорость сглаживания движения

    void Start()
    {
        targetPosition = transform.position; // Изначально цель = текущая позиция камеры
    }

    private void Update()
    {
        #if UNITY_EDITOR
        DetectSwipeMouse();  // Для тестов в редакторе
        #else
        DetectSwipeTouch();  // Для мобильных
        #endif

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

            if (hit.collider.gameObject.name != "Background")
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

                if (swipeVector.magnitude > 0f) // Проверяем длину свайпа
                {
                    MoveCamera(-swipeVector); // Инверсия направления
                }
            }
            isSwiping = false;
        }
    }

    private void DetectSwipeTouch()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:

                    isSwiping = true;

                    startTouchPosition = touch.position;
                    targetPosition = Camera.main.transform.position;
                    transform.position = targetPosition;

                    Vector2 touchPosition = startTouchPosition;

                    touchPosition = Camera.main.ScreenToWorldPoint(startTouchPosition);

                    RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector2.zero);

                    if (hit.collider.gameObject.name != "Background")
                    {
                        touch.phase = TouchPhase.Began;
                        isSwiping = false;
                        return;
                    }
                    break;

                case TouchPhase.Ended:
                    if (!isSwiping)
                        return;

                    endTouchPosition = touch.position;
                    Vector2 swipeVector = endTouchPosition - startTouchPosition;

                    if (swipeVector.magnitude > 50f)
                    {
                        MoveCamera(-swipeVector);
                    }
                    break;
            }
        }
    }

    void MoveCamera(Vector2 direction)
    {
        Vector3 moveDirection = new Vector3(direction.x, 0, 0) * swipeSensitivity;
        targetPosition += moveDirection; // Обновляем целевую позицию
    }
}
