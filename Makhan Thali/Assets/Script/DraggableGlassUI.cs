using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableGlassUI : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public RectTransform krishnaTransform; // Krishna's UI RectTransform
    public GameObject thoughtBubble; // Thought1 UI

    private RectTransform glassTransform;

    private void Start()
    {
        glassTransform = GetComponent<RectTransform>();

        if (thoughtBubble != null)
        {
            thoughtBubble.SetActive(false); // Hide initially
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition; // Move with the mouse
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (RectTransformUtility.RectangleContainsScreenPoint(krishnaTransform, Input.mousePosition))
        {
            if (thoughtBubble != null)
            {
                thoughtBubble.SetActive(true); // Show thought1
            }

            gameObject.SetActive(false); // Hide Glass UI
        }
    }
}