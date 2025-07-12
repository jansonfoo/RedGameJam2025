using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PuzzleItemDandD : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public string itemName;
    public RectTransform spawnZone;
    public Vector2Int[] occupiedCells; // ← добавляем: форма фигуры
    public Vector2 originalPosition;
    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    private GridController grid; // ← ссылка на поле

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GetComponentInParent<Canvas>();
        grid = FindObjectOfType<GridController>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalPosition = rectTransform.anchoredPosition;
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

        if (CanPlaceOnGrid())
        {
            PlaceOnGrid();
        }
        else
        {
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

    private bool CanPlaceOnGrid()
    {
        Vector2Int cellPos = grid.GetCellFromWorldPosition(rectTransform.position);

        foreach (Vector2Int offset in occupiedCells)
        {
            Vector2Int checkPos = cellPos + offset;

            if (!grid.IsInsideGrid(checkPos) || grid.IsOccupied(checkPos))
                return false;
        }

        return true;
    }

    private void PlaceOnGrid()
    {
        Vector2Int cellPos = grid.GetCellFromWorldPosition(rectTransform.position);

        // Отметить клетки как занятые
        foreach (Vector2Int offset in occupiedCells)
        {
            Vector2Int placePos = cellPos + offset;
            grid.MarkOccupied(placePos);
        }

        // Вычислим смещение фигуры относительно минимальной ячейки
        Vector2Int minOffset = GetMinOffset(); // например, (0,1)

        // Смещаем фигуру так, чтобы её визуальный центр совпал с ячейкой
        Vector2Int anchorCell = cellPos - minOffset;
        rectTransform.position = grid.GetWorldPositionFromCell(anchorCell);
    }

    private Vector2Int GetMinOffset()
    {
        int minX = int.MaxValue;
        int minY = int.MaxValue;

        foreach (var offset in occupiedCells)
        {
            if (offset.x < minX) minX = offset.x;
            if (offset.y < minY) minY = offset.y;
        }

        return new Vector2Int(minX, minY);
    }
}

