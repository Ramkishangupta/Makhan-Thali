using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Transform originalParent;
    private Vector3 originalPosition;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>() ?? gameObject.AddComponent<CanvasGroup>(); // Ensure CanvasGroup exists
        originalParent = transform.parent;
        originalPosition = rectTransform.localPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        GameObject plate = GameObject.FindGameObjectWithTag("Plate");

        if (plate != null && RectTransformUtility.RectangleContainsScreenPoint(plate.GetComponent<RectTransform>(), Input.mousePosition, eventData.pressEventCamera))
        {
            string foodName = gameObject.name; // Get the name of the dragged food item
            ActivatePlateItem(foodName);
            rectTransform.SetParent(originalParent);
            rectTransform.localPosition = originalPosition;
        }
        else
        {
            rectTransform.SetParent(originalParent);
            rectTransform.localPosition = originalPosition;
        }
    }

    private void ActivatePlateItem(string foodName)
    {
        GameObject plate = GameObject.FindGameObjectWithTag("Plate");

        if (plate != null)
        {
            foreach (Transform child in plate.transform)
            {
                if (child.name == foodName) // Find the matching inactive food item
                {
                    child.gameObject.SetActive(true); // Activate it on the plate
                    OrderChecker.Instance.AddFoodToPlate(foodName); // Inform OrderChecker
                    break;
                }
            }
        }
    }
}