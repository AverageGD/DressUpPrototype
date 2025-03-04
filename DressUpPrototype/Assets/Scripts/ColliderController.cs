using UnityEngine;

public class ColliderController : MonoBehaviour
{
    // Замораживаем объекты с тегом "Draggable" при входе в триггер
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.CompareTag("Draggable"))
        {
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            rb.velocity = Vector3.zero;    // Сброс скорости
            rb.gravityScale = 0f;          // Отключение гравитации
        }
    }

    // Восстанавливаем физику объектов при выходе из триггера
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision != null && collision.CompareTag("Draggable"))
        {
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            rb.gravityScale = 1f;          // Восстановление гравитации
        }
    }
}