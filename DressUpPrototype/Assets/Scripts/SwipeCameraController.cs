using UnityEngine;

public class SwipeCameraController : MonoBehaviour
{
    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;
    private bool isSwiping = false;
    private Vector3 targetPosition; // ������� ������� ������
    [SerializeField] private float swipeSensitivity = 0.01f; // ���������������� ������
    [SerializeField] private float lerpSpeed = 0.5f; // �������� ����������� ��������

    private void Start()
    {
        targetPosition = transform.position; // ���������� ���� = ������� ������� ������
    }

    private void Update()
    {
        DetectSwipeMouse();

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

            if (hit.collider?.gameObject.name != "Background")
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

                MoveCamera(-swipeVector); // �������� �����������
            }
            isSwiping = false;
        }
    }

    private void MoveCamera(Vector2 direction)
    {
        Vector3 moveDirection = new Vector3(direction.x, 0, 0) * swipeSensitivity;
        targetPosition += moveDirection; // ��������� ������� �������
    }
}
