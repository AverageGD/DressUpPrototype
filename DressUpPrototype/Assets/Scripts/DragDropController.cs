using TMPro;
using UnityEngine;

public class DragDropController : MonoBehaviour
{
    [SerializeField] private LayerMask draggableLayer;

    private bool isDragging = false;

    private GameObject draggableObj;

    private void Update()
    {
#if UNITY_EDITOR
        DragDropMouse();  // Для тестов в редакторе
#else
        DragDropTouch();  // Для мобильных
#endif

    }

    private void DragDropMouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Input.mousePosition;

            Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(mouseWorldPosition, Vector2.zero);

            draggableObj = hit.collider.gameObject;

            if (draggableObj != null && (draggableLayer.value & (1 << draggableObj.layer)) != 0)
            {
                isDragging = true;
            }
        }

        if (isDragging)
        {
            
            Vector2 mousePosition = Input.mousePosition;

            Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

            draggableObj.transform.position = mouseWorldPosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            draggableObj = null;
        }
        
    }


    private void DragDropTouch()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:


                    Vector2 touchPosition = touch.position;

                    Vector2 touchWorldPosition = Camera.main.ScreenToWorldPoint(touchPosition);

                    RaycastHit2D hit = Physics2D.Raycast(touchWorldPosition, Vector2.zero);

                    draggableObj = hit.collider.gameObject;

                    if (draggableObj != null && (draggableLayer.value & (1 << draggableObj.layer)) != 0)
                    {
                        isDragging = true;
                    }
                    break;

                case TouchPhase.Ended:
                    isDragging = false;
                    draggableObj = null;

                    break;
            }

            if (isDragging)
            {

                Vector2 mousePosition = Input.mousePosition;

                Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

                draggableObj.transform.position = mouseWorldPosition;
            }
        }
    }
}
