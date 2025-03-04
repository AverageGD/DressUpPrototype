using UnityEngine;

public class DragDropController : MonoBehaviour
{
    [SerializeField] private LayerMask draggableLayer; // ���� ��� ��������������� ��������

    private bool isDragging = false;
    private GameObject draggableObj; // ������� ��������������� ������

    private void Update()
    {
        DragDropMouse();
    }

    private void DragDropMouse()
    {
        // ������ ��������������
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(mouseWorldPosition, Vector2.zero);
            draggableObj = hit.collider?.gameObject;

            // �������� ���������� ���� ������� � draggableLayer
            if (draggableObj != null && (draggableLayer.value & (1 << draggableObj.layer)) != 0)
            {
                isDragging = true;
            }
            else
            {
                draggableObj = null;
                return;
            }
        }

        // ������� ��������������
        if (isDragging)
        {
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPosition.z = -10; // ������������� Z-���������� ��� ��������� �������
            draggableObj.transform.position = mouseWorldPosition;
        }

        // ��������� ��������������
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;

            if (draggableObj != null)
                draggableObj.GetComponent<Rigidbody2D>().velocity = Vector2.zero; // ����� ��������

            draggableObj = null;
        }
    }
}