using UnityEngine;

public class DragDropController : MonoBehaviour
{
    [SerializeField] private LayerMask draggableLayer; // Слой для перетаскиваемых объектов

    private bool isDragging = false;
    private GameObject draggableObj; // Текущий перетаскиваемый объект

    private void Update()
    {
        DragDropMouse();
    }

    private void DragDropMouse()
    {
        // Начало перетаскивания
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(mouseWorldPosition, Vector2.zero);
            draggableObj = hit.collider?.gameObject;

            // Проверка совпадения слоя объекта с draggableLayer
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

        // Процесс перетаскивания
        if (isDragging)
        {
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPosition.z = -10; // Фиксированная Z-координата для видимости объекта
            draggableObj.transform.position = mouseWorldPosition;
        }

        // Окончание перетаскивания
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;

            if (draggableObj != null)
                draggableObj.GetComponent<Rigidbody2D>().velocity = Vector2.zero; // Сброс скорости

            draggableObj = null;
        }
    }
}