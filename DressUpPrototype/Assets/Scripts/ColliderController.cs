using UnityEngine;

public class ColliderController : MonoBehaviour
{
    // ������������ ������� � ����� "Draggable" ��� ����� � �������
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.CompareTag("Draggable"))
        {
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            rb.velocity = Vector3.zero;    // ����� ��������
            rb.gravityScale = 0f;          // ���������� ����������
        }
    }

    // ��������������� ������ �������� ��� ������ �� ��������
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision != null && collision.CompareTag("Draggable"))
        {
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            rb.gravityScale = 1f;          // �������������� ����������
        }
    }
}