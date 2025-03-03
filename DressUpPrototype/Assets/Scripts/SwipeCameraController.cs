using UnityEngine;

public class SwipeCameraController : MonoBehaviour
{
    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;
    private bool isSwiping = false;
    private Vector3 targetPosition; // ������� ������� ������
    public float swipeSensitivity = 0.1f; // ���������������� ������
    public float lerpSpeed = 5f; // �������� ����������� ��������

    void Start()
    {
        targetPosition = transform.position; // ���������� ���� = ������� ������� ������
    }

    private void Update()
    {
        #if UNITY_EDITOR
        DetectSwipeMouse();  // ��� ������ � ���������
        #else
        DetectSwipeTouch();  // ��� ���������
        #endif

        // ������� �������� ������
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * lerpSpeed);
    }

    private void DetectSwipeMouse()
    {
        if (Input.GetMouseButtonDown(0)) // ������ ������
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
        if (Input.GetMouseButtonUp(0)) // ����� ������
        {
            if (isSwiping)
            {
                endTouchPosition = Input.mousePosition;
                Vector2 swipeVector = endTouchPosition - startTouchPosition;

                if (swipeVector.magnitude > 0f) // ��������� ����� ������
                {
                    MoveCamera(-swipeVector); // �������� �����������
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
        targetPosition += moveDirection; // ��������� ������� �������
    }
}
