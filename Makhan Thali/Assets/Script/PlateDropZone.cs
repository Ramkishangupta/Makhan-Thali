using UnityEngine;
using UnityEngine.EventSystems;

public class PlateDropZone : MonoBehaviour, IDropHandler
{
    private CircleCollider2D plateCollider;

    void Start()
    {
        plateCollider = GetComponent<CircleCollider2D>(); // Ensure the collider exists
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedObject = eventData.pointerDrag;
        if (droppedObject != null)
        {
            RectTransform foodRect = droppedObject.GetComponent<RectTransform>();
            if (IsInsidePlate(foodRect.position))
            {
                droppedObject.transform.SetParent(transform);
            }
        }
    }

    private bool IsInsidePlate(Vector3 position)
    {
        return plateCollider.OverlapPoint(position);
    }
}
