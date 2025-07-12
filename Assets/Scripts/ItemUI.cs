using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public string itemName;
    public CategoryType itemType;
    public RectTransform spawnZone;
    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;

    [HideInInspector]
    public Vector2 originalPosition;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GetComponentInParent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalPosition = rectTransform.anchoredPosition; // запомнить позицию
        canvasGroup.blocksRaycasts = false;
        transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        if (!IsInsideZone())
        {
            //  Вне поля — вернём обратно
            ReturnToOriginalPosition();
        }
    }

    public void ReturnToOriginalPosition()
    {
        rectTransform.anchoredPosition = originalPosition;
    }
    
    private bool IsInsideZone()
    {
        Vector3[] worldCorners = new Vector3[4];
        spawnZone.GetWorldCorners(worldCorners);

        Vector3 itemPos = rectTransform.position;
        return itemPos.x >= worldCorners[0].x && itemPos.x <= worldCorners[2].x &&
               itemPos.y >= worldCorners[0].y && itemPos.y <= worldCorners[2].y;
    }
}
